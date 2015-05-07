using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stereo_matching
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //{
            //    label1.Text = "Hello World";
            //    // ファイルから画像読み込み
            //    var img = OpenCV.Net.CV.LoadImage(@"\\k-nas1\share\MM演習\2015\sample.jpg", OpenCV.Net.LoadImageFlags.AnyColor);
            //    // エッジ画像格納用の変数作成
            //    var dst = img.Clone();

            //    // sobelフィルタ適用
            //    OpenCV.Net.CV.Sobel(img, dst, 1, 1);

            //    // 原画用のウインドウ生成
            //    var windowOrg = new OpenCV.Net.NamedWindow("原画");
            //    // 原画表示
            //    windowOrg.ShowImage(img);
            //    // エッジ用のウィンドウ生成
            //    var windowEdge = new OpenCV.Net.NamedWindow("エッジ");
            //    // エッジ表示
            //    windowEdge.ShowImage(dst);

            //    // キー入力待ち
            //    OpenCV.Net.CV.WaitKey(0);
            //}
            {
                // 入力画像の読み込み
                using (IplImage imgLeft = new IplImage(@"\\k-nas1\share\MM演習\2015\stereo\scene1.row3.colL.png", LoadMode.GrayScale))
                using (IplImage imgRight = new IplImage(@"\\k-nas1\share\MM演習\2015\stereo\scene1.row3.colR.png", LoadMode.GrayScale))
                {
                    // 視差画像, 出力画像の領域を確保
                    using (IplImage dispBM = new IplImage(imgLeft.Size, BitDepth.S16, 1))
                    using (IplImage dispLeft = new IplImage(imgLeft.Size, BitDepth.S16, 1))
                    using (IplImage dispRight = new IplImage(imgLeft.Size, BitDepth.S16, 1))
                    using (IplImage dstBM = new IplImage(imgLeft.Size, BitDepth.U8, 1))
                    using (IplImage dstGC = new IplImage(imgLeft.Size, BitDepth.U8, 1))
                    {
                        // 距離計測とスケーリング  
                        using (CvStereoBMState stateBM = new CvStereoBMState(StereoBMPreset.Basic, 16))
                        using (CvStereoGCState stateGC = new CvStereoGCState(16, 2))
                        {
                            Cv.FindStereoCorrespondenceBM(imgLeft, imgRight, dispBM, stateBM);
                            Cv.FindStereoCorrespondenceGC(imgLeft, imgRight, dispLeft, dispRight, stateGC, false);

                            Cv.ConvertScale(dispBM, dstBM, 1);
                            Cv.ConvertScale(dispLeft, dstGC, -16);

                            using (CvWindow windowBM = new CvWindow("Stereo Correspondence BM", dstBM))
                            using (CvWindow windowGC = new CvWindow("Stereo Correspondence GC", dstGC))
                            {
                                Cv.WaitKey();
                            }
                        }
                    }
                }
            }
        }
    }
}
