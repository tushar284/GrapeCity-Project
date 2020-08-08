using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1Tile;
using C1.C1Pdf;

namespace ImageGalleryUsingDynamicControl
{
    public partial class ImageGallery : Form
    {
        DataFetcher datafetch = new DataFetcher();
        List<ImageItem> imagesList;
        int checkedItems = 0;
        C1PdfDocument imagePdfDocument = new C1PdfDocument();
        Group group1 = new Group();
        Tile tile1 = new Tile();
        Tile tile2 = new Tile();
        Tile tile3 = new Tile();
        Tile tile4 = new Tile();
        PanelElement PE = new PanelElement();
        ImageElement IE = new ImageElement();
        TextElement TE = new TextElement();


        TextBox tb = new TextBox();
        StatusStrip strip = new StatusStrip();
        ToolStripProgressBar pbar = new ToolStripProgressBar();
        Panel p1 = new Panel();
        Panel p2 = new Panel();
        PictureBox pb = new PictureBox();
        PictureBox pbExport = new PictureBox();
        TableLayoutPanel tlp = new TableLayoutPanel();
        SplitContainer sc = new SplitContainer();
        C1TileControl tile = new C1TileControl();
        C1PdfDocument pdf = new C1PdfDocument();

        public ImageGallery()
        {
    
            #region Form Properties
            this.Text = "ImageGallery";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.Size = new Size(780, 800);
            this.MaximumSize = new Size(810, 810);
            this.ShowIcon = false;
            #endregion

            #region SearchBox
            tb.Name = "_searchBox";
            tb.Text = "Search Image";
            tb.BorderStyle = BorderStyle.None;
            tb.Location = new System.Drawing.Point(16, 9);
            tb.Dock = DockStyle.Fill;
            tb.Size = new Size(244, 16);
            tb.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);
            #endregion

            #region PictureBox

            pb.Name = "_search";
            pb.Dock = DockStyle.Left;
            pb.Location = new System.Drawing.Point(0, 0);
            pb.Margin = new Padding(0, 0, 0, 0);
            pb.Size = new Size(40, 16);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Image = Image.FromFile(@"C:\Users\tyagi\Downloads\search.jpg");
            pb.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);
            pb.Click += new EventHandler(this._search_Click);


            pbExport.Name = "_exportImage";
            pbExport.Location = new System.Drawing.Point(29, 3);
            pbExport.Size = new Size(135, 28);
            pbExport.SizeMode = PictureBoxSizeMode.StretchImage;
            pbExport.Image = Image.FromFile(@"C:\Users\tyagi\Downloads\download (1).png");
            pbExport.Visible = false;
            pbExport.Click += new EventHandler(this._exportImage_Click);
            pbExport.Paint += new PaintEventHandler(this._exportImage_Paint);
            #endregion

            #region Panels


            p1.Size = new Size(287, 40);
            p1.Location = new System.Drawing.Point(477, 0);
            p1.Dock = DockStyle.Fill;
            p1.Controls.Add(tb);
            p1.Paint += new PaintEventHandler(this.panel1_Paint);

            p2.Size = new Size(40, 16);
            p2.Location = new System.Drawing.Point(479, 12);
            p2.TabIndex = 1;
            p2.Margin = new Padding(2, 12, 45, 12);
            p2.Controls.Add(pb);

            #endregion

            #region Table Layout Panel

            tlp.ColumnCount = 3;
            tlp.Location = new System.Drawing.Point(0, 0);
            tlp.Dock = DockStyle.Fill;
            tlp.RowCount = 1;
            tlp.Visible = true;
            tlp.Size = new Size(800, 40);
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.50F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.50F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.Controls.Add(p1, 1, 0);
            tlp.Controls.Add(p2, 2, 0);

            #endregion

            #region Tiles
            // tile1
            tile1.BackColor = System.Drawing.Color.LightCoral;
            tile1.Name = "tile1";
            tile1.Text = "Tile 1";
     
           
            // tile2 
            tile2.BackColor = System.Drawing.Color.Teal;
            tile2.Name = "tile2";
            tile2.Text = "Tile 2";
           
            // tile3
            tile3.BackColor = System.Drawing.Color.SteelBlue;
            tile3.Name = "tile3";
            tile3.Text = "Tile 3";
           
            // tile4
            tile4.BackColor = System.Drawing.Color.Black;
            tile4.Name = "tile4";
            tile4.Text = "Tile 4";

            #endregion

            #region Group
            group1.Name = "group1";
            group1.Text = "Default";
            group1.Tiles.Add(this.tile1);
            group1.Tiles.Add(this.tile2);
            group1.Tiles.Add(this.tile3);
            group1.Tiles.Add(this.tile4);
            #endregion

            #region PanelElement
            PE.Alignment = ContentAlignment.BottomLeft;
            PE.Margin = new Padding(10, 6, 10, 6);
            PE.Children.Add(IE);
            PE.Children.Add(TE);
            #endregion

            #region TileControl

            tile.Name = "_imageTileControl";
            tile.AllowChecking = true;
            tile.AllowRearranging = true;
            tile.CellHeight = 78;
            tile.CellSpacing = 11;
            tile.CellWidth = 78;
            tile.Dock = DockStyle.Fill;
            tile.Size = new Size(764, 718);
            tile.SurfacePadding = new Padding(12, 4, 12, 4);
            tile.SwipeDistance = 20;
            tile.SwipeRearrangeDistance = 98;
            tile.Groups.Add(group1);
            tile.DefaultTemplate.Elements.Add(PE);
            tile.Location = new Point(0, 0);


            tile.TileChecked += new System.EventHandler<C1.Win.C1Tile.TileEventArgs>(this._ImageTileControl_TileChecked);
            tile.TileUnchecked += new System.EventHandler<C1.Win.C1Tile.TileEventArgs>(this._ImageTileControl_TileUnchecked);
            tile.Paint += new System.Windows.Forms.PaintEventHandler(this._ImageTileControl_Paint);

            #endregion

            #region Strip

            strip.Dock = DockStyle.Bottom;
            strip.Visible = false;
            pbar.Style = ProgressBarStyle.Marquee;
            strip.Items.Add(pbar);

            #endregion

            #region Split Controller
            sc.Dock = DockStyle.Fill;
            sc.Margin = new Padding(2);
            sc.Orientation = Orientation.Horizontal;
            sc.SplitterDistance = 40;
            sc.FixedPanel = FixedPanel.Panel1;
            sc.IsSplitterFixed = true;
            sc.Panel1.Controls.Add(tlp);
            sc.Panel2.Controls.Add(pbExport);
            sc.Panel2.Controls.Add(tile);
            sc.Panel2.Controls.Add(strip);

            #endregion


            this.Controls.Add(sc);
            
            
        }
        private async void _search_Click(object sender, EventArgs e)
        {
            strip.Visible = true;
            imagesList = await
            datafetch.GetImageData(tb.Text);
            AddTiles(imagesList);
            strip.Visible = false;
        }
        private void AddTiles(List<ImageItem> imageList)
        {
            tile.Groups[0].Tiles.Clear();
            foreach (var imageitem in imageList)
            {
                Tile inner_tile = new Tile();
                inner_tile.HorizontalSize = 2;
                inner_tile.VerticalSize = 2;
                tile.Groups[0].Tiles.Add(inner_tile);
                Image img = Image.FromStream(new
                MemoryStream(imageitem.Base64));
                Template tl = new Template();
                ImageElement ie = new ImageElement();
                ie.ImageLayout = ForeImageLayout.Stretch;
                tl.Elements.Add(ie);
                inner_tile.Template = tl;
                inner_tile.Image = img;
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = tb.Bounds;
            r.Inflate(3, 3);
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
        }

        private void _exportImage_Click(object sender, EventArgs e)
        {
            List<Image> images = new List<Image>();
            foreach (Tile inner_tile in tile.Groups[0].Tiles)
            {
                if (inner_tile.Checked)
                {
                    images.Add(inner_tile.Image);
                }
            }
            ConvertToPdf(images);
            SaveFileDialog saveFile = new SaveFileDialog
            {
                DefaultExt = "pdf",
                Filter = "PDF files (*.pdf)|*.pdf*"
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {

                imagePdfDocument.Save(saveFile.FileName);

            }


        }


        private void ConvertToPdf(List<Image> images)
        {
            RectangleF rect = imagePdfDocument.PageRectangle;
            bool firstPage = true;
            foreach (var selectedimg in images)
            {
                if (!firstPage)
                {
                    imagePdfDocument.NewPage();
                }
                firstPage = false;
                rect.Inflate(-72, -72);
                imagePdfDocument.DrawImage(selectedimg, rect);
            }

        }

        private void _exportImage_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(pbExport.Location.X,
pbExport.Location.Y, pbExport.Width, pbExport.Height);
            r.X -= 29;
            r.Y -= 3;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new
           Point(this.Width, 43));
        }

        private void _ImageTileControl_TileChecked(object sender, TileEventArgs e)
        {
            checkedItems++;
            pbExport.Visible = true;

        }

        private void _ImageTileControl_TileUnchecked(object sender, TileEventArgs e)
        {
            checkedItems--;
            pbExport.Visible = checkedItems > 0;
        }

        private void _ImageTileControl_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawLine(p, 0, 43, 800, 43);
        }



    }
}
