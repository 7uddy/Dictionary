﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionary.MVVM.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for DeleteWordView.xaml
    /// </summary>
    public partial class DeleteWordView : UserControl
    {
        public DeleteWordView()
        {
            InitializeComponent();
        }
        private void ComboBoxWords_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.Template.FindName("PART_EditableTextBox", comboBox) is TextBox textBox)
                {
                    textBox.TextChanged += ComboBox_TextChanged;
                }
            }
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBox.IsDropDownOpen = true;
        }

        private void SearchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
