﻿using System;
using System.Data.SqlTypes;
using System.Windows;

namespace WpfBox1
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
            adv= new Adventure();
        }

        private void StartGame()
        {
            outputTB.Text = $"Willkommen zum großen Abenteuer!\r\n";
            outputTB.AppendText($"Du befindest dich an folgenden Ort: {adv.Player.Location.Name}.\r\n");
            outputTB.AppendText($"Du siehst: {adv.Player.Location.Description}.\r\n");
            outputTB.AppendText("Wohin willst du jetzt gehen?\r\n");
            outputTB.AppendText("Klicke auf einen der Richtungsbuttons: Norden, Osten, Süden oder Westen");
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

        private void ShowLocation()
        {
            Wr("[" + adv.Player.Name + "]");
            Wr(" Befindet sich momentan am folgenden Ort: ");
            WrLn(adv.Player.Location.Describe());
        }

        private void LookBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowLocation();
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

        }
    }
}
