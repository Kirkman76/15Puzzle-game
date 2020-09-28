using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15Puzzle
{
    public partial class Form1 : Form
    {
        List<int> positions;
        List<int> winPositions;
        
        TableLayoutPanelCellPosition empty;

        public Form1()
        {
            InitializeComponent();
            //
            positions = new List<int>();
            for (int i = 1; i < 16; i++)
            {
                positions.Add(i);
            }
            positions.Add(0);
            //
            winPositions = new List<int>();
            winPositions = positions.GetRange(0, positions.Count);
            empty = new TableLayoutPanelCellPosition(3, 3);
            Shuffle();
        }

        private void Shuffle()
        {
            for (int i = 0; i < 50000; i++)
            {
                int index = 0;
                Random generator = new Random();
                index = generator.Next(1, 5);

                Control empt = tableLayoutPanel1.GetControlFromPosition(empty.Column, empty.Row);
                if (empty.Column + 1 < tableLayoutPanel1.ColumnCount && index == 1)
                {
                    Control num = tableLayoutPanel1.GetControlFromPosition(empty.Column + 1, empty.Row);
                    ChangeCellsShuffle(empty, empt, num);
                    empty.Column++;
                }

                if (empty.Column - 1 >= 0 && index == 2)
                {
                    Control num = tableLayoutPanel1.GetControlFromPosition(empty.Column - 1, empty.Row);
                    ChangeCellsShuffle(empty, empt, num);
                    empty.Column--;
                }

                if (empty.Row + 1 < tableLayoutPanel1.RowCount && index == 3)
                {
                    Control num = tableLayoutPanel1.GetControlFromPosition(empty.Column, empty.Row + 1);
                    ChangeCellsShuffle(empty, empt, num);
                    empty.Row++;
                }

                if (empty.Row - 1 >= 0 && index == 4)
                {
                    Control num = tableLayoutPanel1.GetControlFromPosition(empty.Column, empty.Row - 1);
                    ChangeCellsShuffle(empty, empt, num);
                    empty.Row--;
                }

            }
            
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            TableLayoutPanelCellPosition numPos = tableLayoutPanel1.GetCellPosition((Control)sender);
            Control num = sender as Control;
            if (numPos.Column + 1 < tableLayoutPanel1.ColumnCount)
            {
                Control empt = tableLayoutPanel1.GetControlFromPosition(numPos.Column + 1, numPos.Row);
                ChangeCells(numPos, num, empt);
            }

            if (numPos.Column - 1 >= 0)
            {
                Control empt = tableLayoutPanel1.GetControlFromPosition(numPos.Column - 1, numPos.Row);
                ChangeCells(numPos, num, empt);
            }

            if (numPos.Row + 1 < tableLayoutPanel1.RowCount)
            {
                Control empt = tableLayoutPanel1.GetControlFromPosition(numPos.Column, numPos.Row + 1);
                ChangeCells(numPos, num, empt);
            }

            if (numPos.Row - 1 >= 0)
            {
                Control empt = tableLayoutPanel1.GetControlFromPosition(numPos.Column, numPos.Row - 1);
                ChangeCells(numPos, num, empt);
            }

            if (positions.SequenceEqual(winPositions))
            {
                MessageBox.Show("WIN");
            }

        }

        private void ChangeCells(TableLayoutPanelCellPosition numPos, Control num, Control empt)
        {
            if (empt.Tag.ToString() == "Empty")
            {
                TableLayoutPanelCellPosition empPosition = tableLayoutPanel1.GetCellPosition(empt);
                tableLayoutPanel1.SetCellPosition(empt, numPos);
                tableLayoutPanel1.SetCellPosition(num, empPosition);
                int posEmp = positions.IndexOf(0);
                int posNum = positions.IndexOf(Convert.ToInt32(num.Tag));
                int tmp = positions[posEmp];
                empty = tableLayoutPanel1.GetCellPosition(empt);
                positions[posEmp] = positions[posNum];
                positions[posNum] = tmp;
            }
        }

        private void ChangeCellsShuffle(TableLayoutPanelCellPosition numPos, Control num, Control empt)
        {
                TableLayoutPanelCellPosition empPosition = tableLayoutPanel1.GetCellPosition(empt);
                tableLayoutPanel1.SetCellPosition(empt, numPos);
                tableLayoutPanel1.SetCellPosition(num, empPosition);
                int posNum = positions.IndexOf(0);
                int posEmp = positions.IndexOf(Convert.ToInt32(empt.Tag));
                int tmp = positions[posEmp];
                positions[posEmp] = positions[posNum];
                positions[posNum] = tmp;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
                Shuffle();
        }
    }
}
