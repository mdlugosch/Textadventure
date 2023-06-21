﻿using Microsoft.Win32;
using System;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace WpfBox3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Adventure adv;
        public MainWindow()
        {
            InitializeComponent();
            InitGame();
            StartGame();
        }

        /*
        * Trollraum -- Wald
        *    |
        * Höhle ------ Verlies
        */
        private void InitGame()
        {
            adv = new Adventure();
        }

        private void StartGame()
        {
            outputTB.Text = $"Willkommen zum großen Abenteuer!\r\n";
            outputTB.AppendText($"Du befindest dich an folgenden Ort: {adv.Player.Location.Name}.\r\n");
            outputTB.AppendText($"Du siehst: {adv.Player.Location.Description}.\r\n");
            outputTB.AppendText("Wohin willst du jetzt gehen?\r\n");
            outputTB.AppendText("Klicke auf einen der Richtungsbuttons: Norden, Osten, Süden oder Westen\r\n");
        }

        // Hilfmethode um der Ausgabebox Text hinzuzufügen
        private void Wr(String s)
        {
            outputTB.AppendText(s);
        }

        /*
         * Vor dem Aufruf der Hilfsmethode fügt diese Methode noch einen 
         * Wagenrücklauf (Sprung an den Anfang der aktuellen Zeile) 
         * und Neue Zeile unterhalb der aktuellen.
        */
        private void WrLn(String s)
        {
            Wr(s + "\r\n");
        }

        // --- Nicht genutzt ---
        private void ShowLocation()
        {
            Wr("[" + adv.Player.Name + "]");
            Wr(" Befindet sich momentan am folgenden Ort: ");
            WrLn(adv.Player.Location.Describe());
        }

        private void LookBtn_Click(object sender, RoutedEventArgs e)
        {
            WrLn(adv.Look());
            //ShowLocation();
        }

        private void InventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            WrLn("Du hast folgendes" + adv.Player.Things.Describe());
        }

        private void TakeBtn_Click(object sender, RoutedEventArgs e)
        {
            WrLn(adv.TakeOb(inputTB.Text));
        }

        private void DropBtn_Click(object sender, RoutedEventArgs e)
        {
            WrLn(adv.DropOb(inputTB.Text));
        }

        private void MovePlayer(Dir direction)
        {
            outputTB.Text = adv.MovePlayerTo(direction);
        }

        private void NBtn_Click(object sender, RoutedEventArgs e)
        {
            MovePlayer(Dir.NORDEN);
        }

        private void SBtn_Click(object sender, RoutedEventArgs e)
        {
            MovePlayer(Dir.SUEDEN);
        }

        private void WBtn_Click(object sender, RoutedEventArgs e)
        {
            MovePlayer(Dir.WESTEN);
        }

        private void OBtn_Click(object sender, RoutedEventArgs e)
        {
            MovePlayer(Dir.OSTEN);
        }

        private void LookAtBtn_Click(object sender, RoutedEventArgs e)
        {
            WrLn(adv.LookAtOb(inputTB.Text));
        }
        
        private void loadToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            Stream st;
            BinaryFormatter binfmt;
            if (openFileDialog1.ShowDialog() == true)
            {
                if ((st = openFileDialog1.OpenFile()) != null)
                {
                    binfmt = new BinaryFormatter();
                    adv = (Adventure)binfmt.Deserialize(st);
                    st.Close();
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Mit der Welt interagieren:\r\n [Verb] [Objekt] z.B. \"Nehme Objekt\" oder \r\n [Verb] [Artikel] [Objekt] z.B. \"Nehme das Objekt\"";
            string caption = "Anleitung";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.None;

            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
        }

        private void saveToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            Stream st;
            BinaryFormatter binfmt;
            if(saveFileDialog1.ShowDialog() == true)
            {
                if((st = saveFileDialog1.OpenFile()) != null) {
                    binfmt = new BinaryFormatter();
                    binfmt.Serialize(st, adv);
                    st.Close();
                    WrLn("gespeichert");
                }
            }
        }

        private void restartToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            InitGame();
        }

        private void exitToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpBtn_Click(object sender, RoutedEventArgs e)
        {
            MovePlayer(Dir.HOCH);
        }

        private void DownBtn_Click(object sender, RoutedEventArgs e)
        {
            MovePlayer(Dir.RUNTER);
        }

        private void cmdBtn_Click(object sender, RoutedEventArgs e)
        {
           cmdTB_KeyDown(sender, new KeyEventArgs(Keyboard.PrimaryDevice,
           Keyboard.PrimaryDevice.ActiveSource,
           0, Key.Enter));
        }

        private void cmdTB_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter) 
            {
                WrLn(adv.RunCommand(cmdTB.Text));
                cmdTB.Clear();
            }
        }
    }
}
