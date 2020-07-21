using AForge.Video;
using AForge.Video.DirectShow;
using OpenCvSharp;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace qrCodePrj
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// 画像フィルタリングのフィルタ値
        /// この値を上げると精度が上がっていくが、処理時間も増加する。
        /// </summary>
        const int maxFilterSize = 10;

        // フィールド
        public bool DeviceExist = false;                // デバイス有無
        public FilterInfoCollection videoDevices;       // カメラデバイスの一覧
        public VideoCaptureDevice videoSource = null;   // カメラデバイスから取得した映像


        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.getCameraInfo();
        }
        #endregion

        #region カメラ情報の取得
        /// <summary>
        /// カメラ情報の取得
        /// </summary>
        public void getCameraInfo()
        {
            try
            {
                // 端末で認識しているカメラデバイスの一覧を取得
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                cmbCamera.Items.Clear();

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                foreach (FilterInfo device in videoDevices)
                {
                    //カメラデバイスの一覧をコンボボックスに追加
                    cmbCamera.Items.Add(device.Name);
                    cmbCamera.SelectedIndex = 0;
                    DeviceExist = true;
                }
            }
            catch (ApplicationException)
            {
                DeviceExist = false;
                cmbCamera.Items.Add("Deviceが存在していません。");
            }

        }
        #endregion

        #region 描画処理
        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void videoRendering(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();

            try
            {
                qrRead(img);
                pictureBoxCamera.Image = img;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        #endregion

        #region 停止の初期化
        /// <summary>
        /// 停止の初期化
        /// </summary>
        private void CloseVideoSource()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
        }
        #endregion

        #region フォームを閉じる処理
        /// <summary>
        /// フォームを閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null)
            {
                // Form を閉じる際は映像データ取得をクローズ
                if (videoSource.IsRunning)
                {
                    this.CloseVideoSource();
                }
            }
        }
        #endregion

        #region 実行ボタン押下
        /// <summary>
        /// 実行ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExec_Click(object sender, EventArgs e)
        {
            if (btnExec.Text == "開始")
            {
                if (cmbCamera.Items.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show("カメラがありません。", "実行エラー");
                    return;
                }
                if (DeviceExist)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[cmbCamera.SelectedIndex].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(videoRendering);
                    this.CloseVideoSource();

                    videoSource.Start();

                    btnExec.Text = "停止";
                }
            }
            else
            {
                if (videoSource.IsRunning)
                {
                    this.CloseVideoSource();
                    btnExec.Text = "開始";

                }
            }
        }
        #endregion

        #region QR読み取り処理
        /// <summary>
        /// QR読み取り処理
        /// </summary>
        /// <param name="image"></param>
        private void qrRead(System.Drawing.Image image)
        {


            try
            {
                Bitmap myBitmap = new Bitmap(image);

                string text = string.Empty;

                using (Mat imageMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(myBitmap))
                {

                    // QRコードの解析
                    ZXing.BarcodeReader reader = new ZXing.BarcodeReader();

                    //テンポラリパス
                    for (int i = 0; i < maxFilterSize; i++)
                    {

                        //奇数にする必要があるので、さらに加算
                        i++;

                        //偶数を指定するとExceptionが発生します。
                        int filterSize = i;

                        //別変数のMATにとる
                        using (Mat imageMatFilter = imageMat.GaussianBlur(new OpenCvSharp.Size(filterSize, filterSize), 0))
                        {
                            //ビットマップに戻します
                            using (Bitmap filterResult = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(imageMatFilter))
                            {
                                try
                                {
                                    //QRコードの解析
                                    ZXing.Result result = reader.Decode(filterResult);

                                    //これでQRコードのテキストが読める
                                    if (result != null)
                                    {
                                        text = result.Text;
                                        System.Windows.Forms.MessageBox.Show(text, "読み取り値"); //メッセージポップアップ
                                        return;
                                    }
                                }
                                catch
                                {
                                }

                            }


                        }

                    }
                }

            }
            catch (Exception ex)
            {
                //システムエラー発生時
                System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                this.Close();
            }
            finally
            {

            }
        }
        #endregion

    }
}
