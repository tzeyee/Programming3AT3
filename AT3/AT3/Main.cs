using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LumenWorks.Framework.IO.Csv; //using LemenWorks as third party library

//Tze Yee Hon P466426
// AT3 05/12/2019

namespace AT3
{
    public partial class Main : Form
    {
        List<string> myGame = new List<string>();
        private int numRow = 0;
        int i;

        //set file path to null, so that we can use openfiledialog to select any file to display
        string filepath = "";

        public Main()
        {
            InitializeComponent();
        }

        //bubble sort
        private void BubbleSort()
        {
            bool inOrder = false;

            while (!inOrder)
            {
                inOrder = true;
                for(i = 0; i < numRow -1; i++)
                {
                    if(string.Compare(myGame[i].ToString(), myGame[i + 1].ToString()) > 0)
                    {
                        Swap();
                        inOrder = false;
                    }
                }
            }
        }

        //swap 
        private void Swap()
        {
            //store temparary data into temp when swap between elements
            string temp = myGame[i];
            myGame[i] = myGame[i + 1];
            myGame[i + 1] = temp;

            string temp1 = myGame[i];
            myGame[i] = myGame[i + 1];
            myGame[i + 1] = temp1;

            string temp2 = myGame[i];
            myGame[i] = myGame[i + 1];
            myGame[i + 1] = temp2;
        }

        //Binary search
        private int BinarySearch(string search)
        {
            int lowerBound = 0; //start of the range
            int upperBound = numRow - 1; //end of the range
            int i;

            while (true)
            {
                i = (lowerBound + upperBound) / 2;
                int j = myGame[i].CompareTo(search);

                // If the element is present at the middle itself
                if (j == 0)
                {
                    return i;
                }
                // If element is smaller than mid, then it can only be present in left subarray 
                else if (lowerBound > upperBound)
                {
                    return -1;
                }
                // Else the element can only be present in right subarray 
                else
                {
                    if (j < 0)
                    {
                        lowerBound = i + 1;
                    }
                    else
                    {
                        upperBound = i - 1;
                    }
                }
            }

        }

        //Display games stored in myGame array
        private void DisplayNames()
        {
            //clear listbox
            lstOutput.Items.Clear();
            for (int i = 0; i < myGame.Count(); i++)
            {
                if (myGame[i] != null)
                {
                    lstOutput.Items.Add(myGame[i].ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Display message box to inform user if the textbox is null or empty
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Please fill in the name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Display message box to inform user if the textbox is null or empty
            if (string.IsNullOrEmpty(tbPlatform.Text))
            {
                MessageBox.Show("Please fill in the platform", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Display message box to inform user if the textbox is null or empty
            if (string.IsNullOrEmpty(tbGenre.Text))
            {
                MessageBox.Show("Please fill in the genre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Game game = new Game(tbName.Text, tbPlatform.Text, tbGenre.Text);
            game.setName(tbName.Text); //add items into list
            game.setPlatform(tbPlatform.Text); //add items into list
            game.setGenre(tbGenre.Text); //add items into list
            myGame.Add(game.ToString());
            numRow++;
            DisplayNames();
            tbName.Clear(); //clear text box after add items
            tbPlatform.Clear(); //clear textbox after add items
            tbGenre.Clear(); //clear textbox after add items
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int search = BinarySearch(tbName.Text); //search by game title
            //display message box to inform user if game not found in list
            if (search == -1)
            {
                MessageBox.Show("Game not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //will select the item if found the item by searching game title
                lstOutput.SelectedIndex = search;
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            //clear list box items before start sorting
            lstOutput.Items.Clear();
            BubbleSort(); //run the bubble sort method
            DisplayNames(); //display item afte sort
        }

        private void OpenFile()
        {
            //open file dialog and select the file that we want to display
            OpenFileDialog open = new OpenFileDialog();
            try
            {
                if(open.ShowDialog() == DialogResult.OK)
                {
                    //set file path when the file is selected
                    filepath = open.FileName;
                }

            }
            catch
            {
                // display message box to inform user when unable to open the selected file
                MessageBox.Show("Unable to open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowFile()
        {
            lstOutput.Items.Clear();
            //open selected csv files with header
            using(CachedCsvReader csv = new CachedCsvReader(new StreamReader(filepath), true))
            {
                int fieldCount = csv.FieldCount;
                //set header for csv file
                string[] header = csv.GetFieldHeaders();

                while (csv.ReadNextRecord())
                {
                    for(int i = 0; i < fieldCount; i++)
                    {
                        //output list box by header then items come after header
                        lstOutput.Items.Add(string.Format("{0} = {1};",
                                        header[i], csv[i]));
                    }
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFile(); //open file dialog to select the file that want to open
            ShowFile(); //display selected file in listbox
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = @"Games.csv"; //save file path as Game.csv
                string delimiter = ","; //seperate sentence by ","

                int length = myGame.Count();
                StringBuilder sb = new StringBuilder("Title,Platform,Genre\r\n"); //set header to save in csv file

                for (int i = 0; i < length; i++)
                {
                    //add items into csv file by sperate sentence with ","
                    sb.AppendLine(String.Join(delimiter, myGame[i]));

                    //save data into csv file
                    File.WriteAllText(filepath, sb.ToString());
                }

                //display message box to inform user when data saved
                MessageBox.Show("Data saved", "", MessageBoxButtons.OK);
            }
            catch (IOException x)
            {
                //display message box when there is error
                MessageBox.Show("Exception: " + x.Message);
            }
        }
    }
}
