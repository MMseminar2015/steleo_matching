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
        //test
        string left = @"\\k-nas1\share\MM演習\2015\stereo\scene1.row3.colL.png";
        string right = @"\\k-nas1\share\MM演習\2015\stereo\scene1.row3.colR.png";

        public Form1()
        {
            InitializeComponent();

            pictureBoxL.Image = new Bitmap(left);
            pictureBoxR.Image = new Bitmap(right);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                // 入力画像の読み込み
                using (IplImage imgLeft = new IplImage(left, LoadMode.GrayScale))
                using (IplImage imgRight = new IplImage(right, LoadMode.GrayScale))
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
