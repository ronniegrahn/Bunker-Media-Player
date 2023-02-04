using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bunker_Media_Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            label1.Text = version;
        }

        #region MusikPlayer1
        private void musicAddButton1_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Multiselect = true
            };

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string[] fileNameAndPath = openFileDialog1.FileNames;
                            string[] filename = openFileDialog1.SafeFileNames;

                            for (int i = 0; i < openFileDialog1.SafeFileNames.Count(); i++)
                            {
                                string[] delaMediaNamn = new string[2];

                                delaMediaNamn[0] = filename[i];
                                delaMediaNamn[1] = fileNameAndPath[i];

                                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                                musicList1.Items.Add(lvi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Error code: " + ex);
                }
            }
        }

        private void musicList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMedia = musicList1.FocusedItem.SubItems[1].Text;
            musicPlayer1.URL = @selectedMedia;
        }

        private void buttonPlayAll1_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist1 = musicPlayer1.playlistCollection.newPlaylist("playlist1");
            WMPLib.IWMPMedia media;

            for (int i = 0; i < musicList1.Items.Count; i++)
            {
                int ii = 1;
                media = musicPlayer1.newMedia(musicList1.Items[i].SubItems[ii].Text);
                playlist1.appendItem(media);
                ii++;

                musicPlayer1.currentPlaylist = playlist1;
                musicPlayer1.Ctlcontrols.play();
            }
        }

        private void musicPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (musicPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped && checkRepeatSong1.Checked == true)
            {
                musicPlayer1.Ctlcontrols.play();
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //musicList1.Items.Remove(musicList1.SelectedItems[0]);

            foreach (ListViewItem item in musicList1.SelectedItems)
            {
                item.Remove();
            }
        }

        private void MusicList1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void MusicList1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < s.Length; i++)
            {
                string[] delaMediaNamn = new string[2];

                delaMediaNamn[0] = Path.GetFileName(s[i]);
                delaMediaNamn[1] = s[i];

                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                musicList1.Items.Add(lvi);
            }
                //musicList1.Items.Add(s[i]);
        }

        private void buttonSavePlaylist_Click(object sender, EventArgs e)
        {
            StreamWriter streamWriter;
            SaveFileDialog savePlaylist = new SaveFileDialog();
            WMPLib.IWMPMedia media;

            try
            {
                savePlaylist.Filter = ("XML File|*.xml|All Files|*.*");
                savePlaylist.ShowDialog();
                streamWriter = new StreamWriter(savePlaylist.FileName);
                for (int i = 0; i < musicList1.Items.Count; i++)
                {
                    int ii = 1;
                    media = musicPlayer1.newMedia(musicList1.Items[i].SubItems[ii].Text);
                    streamWriter.WriteLine(media);
                    ii++;
                    
                }
                streamWriter.Close();
                MessageBox.Show("Playlist saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Trying to save playlist. Error code: " + ex);
            }
        }
        #endregion

        #region MusikPlayer2
        private void musicAddButton2_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog2 = new OpenFileDialog();

            openFileDialog2.Multiselect = true;

            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog2.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string[] fileNameAndPath = openFileDialog2.FileNames;
                            string[] filename = openFileDialog2.SafeFileNames;

                            for (int i = 0; i < openFileDialog2.SafeFileNames.Count(); i++)
                            {
                                string[] delaMediaNamn = new string[2];

                                delaMediaNamn[0] = filename[i];
                                delaMediaNamn[1] = fileNameAndPath[i];

                                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                                musicList2.Items.Add(lvi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Error code: " + ex);
                }
            }
        }

        private void buttonPlayAll2_Click_1(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist2 = musicPlayer2.playlistCollection.newPlaylist("playlist2");
            WMPLib.IWMPMedia media;

            for (int i = 0; i < musicList2.Items.Count; i++)
            {
                int ii = 1;
                media = musicPlayer2.newMedia(musicList2.Items[i].SubItems[ii].Text);
                playlist2.appendItem(media);
                ii++;

                musicPlayer2.currentPlaylist = playlist2;
                musicPlayer2.Ctlcontrols.play();
            }
        }

        private void musicList2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedMedia = musicList2.FocusedItem.SubItems[1].Text;
            musicPlayer2.URL = @selectedMedia;

        }

        private void musicPlayer2_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (musicPlayer2.playState == WMPLib.WMPPlayState.wmppsStopped && checkRepeatSong2.Checked == true)
            {
                musicPlayer2.Ctlcontrols.play();
            }
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in musicList2.SelectedItems)
            {
                item.Remove();
            }
        }

        private void MusicList2_DragDrop(object sender, DragEventArgs e)
        {

            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < s.Length; i++)
            {
                string[] delaMediaNamn = new string[2];

                delaMediaNamn[0] = Path.GetFileName(s[i]);
                delaMediaNamn[1] = s[i];

                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                musicList2.Items.Add(lvi);
            }
            
        }

        private void MusicList2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        #endregion

        #region MusikPlayer3
        private void musicAddButton3_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog3 = new OpenFileDialog();

            openFileDialog3.Multiselect = true;

            if (openFileDialog3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog3.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string[] fileNameAndPath = openFileDialog3.FileNames;
                            string[] filename = openFileDialog3.SafeFileNames;

                            for (int i = 0; i < openFileDialog3.SafeFileNames.Count(); i++)
                            {
                                string[] delaMediaNamn = new string[2];

                                delaMediaNamn[0] = filename[i];
                                delaMediaNamn[1] = fileNameAndPath[i];

                                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                                musicList3.Items.Add(lvi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Error code: " + ex);
                }
            }
        }

        private void buttonPlayAll3_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist3 = musicPlayer3.playlistCollection.newPlaylist("playlist3");
            WMPLib.IWMPMedia media;

            for (int i = 0; i < musicList3.Items.Count; i++)
            {
                int ii = 1;
                media = musicPlayer3.newMedia(musicList3.Items[i].SubItems[ii].Text);
                playlist3.appendItem(media);
                ii++;

                musicPlayer3.currentPlaylist = playlist3;
                musicPlayer3.Ctlcontrols.play();
            }
        }

        private void musicList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMedia = musicList3.FocusedItem.SubItems[1].Text;
            musicPlayer3.URL = @selectedMedia;
        }

        private void musicPlayer3_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (musicPlayer3.playState == WMPLib.WMPPlayState.wmppsStopped && checkRepeatSong3.Checked == true)
            {
                musicPlayer3.Ctlcontrols.play();
            }
        }

        private void removeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in musicList3.SelectedItems)
            {
                item.Remove();
            }
        }

        private void MusicList3_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < s.Length; i++)
            {
                string[] delaMediaNamn = new string[2];

                delaMediaNamn[0] = Path.GetFileName(s[i]);
                delaMediaNamn[1] = s[i];

                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                musicList3.Items.Add(lvi);
            }
        }

        private void MusicList3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        #endregion

        #region MusikPlayer4
        private void musicAddButton4_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog4 = new OpenFileDialog();

            openFileDialog4.Multiselect = true;

            if (openFileDialog4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog4.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string[] fileNameAndPath = openFileDialog4.FileNames;
                            string[] filename = openFileDialog4.SafeFileNames;

                            for (int i = 0; i < openFileDialog4.SafeFileNames.Count(); i++)
                            {
                                string[] delaMediaNamn = new string[2];

                                delaMediaNamn[0] = filename[i];
                                delaMediaNamn[1] = fileNameAndPath[i];

                                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                                musicList4.Items.Add(lvi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Error code: " + ex);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist4 = musicPlayer4.playlistCollection.newPlaylist("playlist4");
            WMPLib.IWMPMedia media;

            for (int i = 0; i < musicList4.Items.Count; i++)
            {
                int ii = 1;
                media = musicPlayer4.newMedia(musicList4.Items[i].SubItems[ii].Text);
                playlist4.appendItem(media);
                ii++;

                musicPlayer4.currentPlaylist = playlist4;
                musicPlayer4.Ctlcontrols.play();
            }
        }

        private void musicList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMedia = musicList4.FocusedItem.SubItems[1].Text;
            musicPlayer4.URL = @selectedMedia;
        }

        private void musicPlayer4_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (musicPlayer4.playState == WMPLib.WMPPlayState.wmppsStopped && checkRepeatSong4.Checked == true)
            {
                musicPlayer4.Ctlcontrols.play();
            }
        }

        private void removeToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in musicList4.SelectedItems)
            {
                item.Remove();
            }
        }

        private void MusicList4_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < s.Length; i++)
            {
                string[] delaMediaNamn = new string[2];

                delaMediaNamn[0] = Path.GetFileName(s[i]);
                delaMediaNamn[1] = s[i];

                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                musicList4.Items.Add(lvi);
            }
        }

        private void MusicList4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        #endregion

        #region MusikPlayer5
        private void musicAddButton5_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog5 = new OpenFileDialog();

            openFileDialog5.Multiselect = true;

            if (openFileDialog5.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog5.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string[] fileNameAndPath = openFileDialog5.FileNames;
                            string[] filename = openFileDialog5.SafeFileNames;

                            for (int i = 0; i < openFileDialog5.SafeFileNames.Count(); i++)
                            {
                                string[] delaMediaNamn = new string[2];

                                delaMediaNamn[0] = filename[i];
                                delaMediaNamn[1] = fileNameAndPath[i];

                                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                                musicList5.Items.Add(lvi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Error code: " + ex);
                }
            }
        }

        private void buttonPlayAll5_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist5 = musicPlayer5.playlistCollection.newPlaylist("playlist5");
            WMPLib.IWMPMedia media;

            for (int i = 0; i < musicList5.Items.Count; i++)
            {
                int ii = 1;
                media = musicPlayer5.newMedia(musicList5.Items[i].SubItems[ii].Text);
                playlist5.appendItem(media);
                ii++;

                musicPlayer5.currentPlaylist = playlist5;
                musicPlayer5.Ctlcontrols.play();
            }
        }

        private void musicList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMedia = musicList5.FocusedItem.SubItems[1].Text;
            musicPlayer5.URL = @selectedMedia;
        }

        private void musicPlayer5_PlaylistChange(object sender, AxWMPLib._WMPOCXEvents_PlaylistChangeEvent e)
        {           
        }

        private void musicPlayer5_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (musicPlayer5.playState == WMPLib.WMPPlayState.wmppsStopped && checkRepeatSong5.Checked == true)
            {
                musicPlayer5.Ctlcontrols.play();
            }
        }

        private void removeToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in musicList5.SelectedItems)
            {
                item.Remove();
            }
        }

        private void MusicList5_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < s.Length; i++)
            {
                string[] delaMediaNamn = new string[2];

                delaMediaNamn[0] = Path.GetFileName(s[i]);
                delaMediaNamn[1] = s[i];

                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                musicList5.Items.Add(lvi);
            }
        }

        private void MusicList5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        #endregion

        #region MusikPlayer6
        private void musicAddButton6_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog6 = new OpenFileDialog();

            openFileDialog6.Multiselect = true;

            if (openFileDialog6.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog6.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string[] fileNameAndPath = openFileDialog6.FileNames;
                            string[] filename = openFileDialog6.SafeFileNames;

                            for (int i = 0; i < openFileDialog6.SafeFileNames.Count(); i++)
                            {
                                string[] delaMediaNamn = new string[2];

                                delaMediaNamn[0] = filename[i];
                                delaMediaNamn[1] = fileNameAndPath[i];

                                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                                musicList6.Items.Add(lvi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Error code: " + ex);
                }
            }
        }

        private void buttonPlayAll6_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist6 = musicPlayer6.playlistCollection.newPlaylist("playlist6");
            WMPLib.IWMPMedia media;

            for (int i = 0; i < musicList6.Items.Count; i++)
            {
                int ii = 1;
                media = musicPlayer6.newMedia(musicList6.Items[i].SubItems[ii].Text);
                playlist6.appendItem(media);
                ii++;

                musicPlayer6.currentPlaylist = playlist6;
                musicPlayer6.Ctlcontrols.play();
            }
        }

        private void musicList6_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMedia = musicList6.FocusedItem.SubItems[1].Text;
            musicPlayer6.URL = @selectedMedia;
        }

        private void musicPlayer6_PlaylistChange(object sender, AxWMPLib._WMPOCXEvents_PlaylistChangeEvent e)
        {   
        }
        

        private void musicPlayer6_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (musicPlayer6.playState == WMPLib.WMPPlayState.wmppsStopped && checkRepeatSong6.Checked == true)
            {
                musicPlayer6.Ctlcontrols.play();
            }
        }

        private void removeToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in musicList6.SelectedItems)
            {
                item.Remove();
            }
        }

        private void MusicList6_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < s.Length; i++)
            {
                string[] delaMediaNamn = new string[2];

                delaMediaNamn[0] = Path.GetFileName(s[i]);
                delaMediaNamn[1] = s[i];

                ListViewItem lvi = new ListViewItem(delaMediaNamn);

                musicList6.Items.Add(lvi);
            }
        }

        private void MusicList6_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        #endregion




        //TEST OCH ANNAT ÖVERBLIVET
        private void musicList1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (Keys.Delete == e.KeyCode)
                
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //    e.Effect = DragDropEffects.All;
            //else
            //    e.Effect = DragDropEffects.None;
        }
    }
}
