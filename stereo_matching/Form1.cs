﻿using System;
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
            label1.Text = "Hello World";
            // ファイルから画像読み込み
            var img = OpenCV.Net.CV.LoadImage(@"\\k-nas1\share\MM演習\2015\sample.jpg", OpenCV.Net.LoadImageFlags.AnyColor);
            // エッジ画像格納用の変数作成
            var dst = img.Clone();

            // sobelフィルタ適用
            OpenCV.Net.CV.Sobel(img, dst, 1, 1);

            // 原画用のウインドウ生成
            var windowOrg = new OpenCV.Net.NamedWindow("原画");
            // 原画表示
            windowOrg.ShowImage(img);
            // エッジ用のウィンドウ生成
            var windowEdge = new OpenCV.Net.NamedWindow("エッジ");
            // エッジ表示
            windowEdge.ShowImage(dst);

            // キー入力待ち
            OpenCV.Net.CV.WaitKey(0);

        }
    }
}