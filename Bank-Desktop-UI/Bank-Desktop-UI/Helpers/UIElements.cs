using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;

namespace Bank_Desktop_UI.Helpers
{
    public static class UIElements
    {
        public static Label CreateLabel(string Content, int RowNr = 0, int ColumnNr = 0, VerticalAlignment VerticalContentAlightment = VerticalAlignment.Top, HorizontalAlignment HorizontalContentAlignment = HorizontalAlignment.Left, int RowSpan = 1, int ColumnSpan = 1, FontWeight? TextWeight = null)
        {
            Label label = new Label
            {
                Content = Content
            };
            Grid.SetRow(label, RowNr);
            Grid.SetColumn(label, ColumnNr);
            Grid.SetRowSpan(label, RowSpan);
            Grid.SetColumnSpan(label, ColumnSpan);
            label.VerticalContentAlignment = VerticalContentAlightment;
            label.HorizontalContentAlignment = HorizontalContentAlignment;
            label.FontWeight = FontWeights.Normal;

            if (TextWeight.HasValue)
                label.FontWeight = TextWeight.Value;

            return label;
        }

        public static Grid GetBaseGrid(int Rows, int Columns, int[]? RowDefinitionStarLengths = null , int[]? ColumnDefinitionStarLengths = null)
        {
            Grid grid = new();
            if (RowDefinitionStarLengths != null && RowDefinitionStarLengths.Any())
            {
                foreach (var rowLength in RowDefinitionStarLengths)
                {
                    var row = new RowDefinition
                    {
                        Height = new GridLength(rowLength, GridUnitType.Star),
                    };
                    grid.RowDefinitions.Add(row);
                }
            }
            else
            {
                for (int rowCount = 0; rowCount < Rows; rowCount++)
                {
                    var row = new RowDefinition();
                    row.Height = new GridLength(1.0, GridUnitType.Star);
                    grid.RowDefinitions.Add(row);
                }
            }

            if (ColumnDefinitionStarLengths != null && ColumnDefinitionStarLengths.Any())
            {
                foreach (var columnLength in ColumnDefinitionStarLengths)
                {
                    var column = new ColumnDefinition
                    {
                        Width = new GridLength(columnLength, GridUnitType.Star),
                    };
                    grid.ColumnDefinitions.Add(column);
                }
            }
            else
            {
                for (int ColumnCount = 0; ColumnCount < Columns; ColumnCount++)
                {
                    var column = new ColumnDefinition();
                    column.Width = new GridLength(1.0, GridUnitType.Star);
                    grid.ColumnDefinitions.Add(column);
                }
            }

            return grid;
        }

    }
}
