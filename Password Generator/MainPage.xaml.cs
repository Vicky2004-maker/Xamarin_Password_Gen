/////////////////////////////////////////////////////////   
// Vicky2004-maker/Xamarin_Password_Gen @ GitHub    
// Apache License 2.0               
// Developed by Vicky
// Difficulty - Beginner
/////////////////////////////////////////////////////////

using Password_Generator.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Password_Generator
{
    public partial class MainPage : ContentPage
    {
        private bool upper, lower, number, special, empty; // boolean created to store the value of CheckChanged Events
        private string generatedPassword; // string to temporarily save the generated password 
        private int passwordLength; // This is created to validate the password length given by the user

        public MainPage()
        {
            InitializeComponent();
        }

        // This is the TextChanged Event
        // This it to Enable the Generate Button in the UI if the user has entered atleast a single digit number as length
        // This will simply rectify ArgumentNullException

        private void inputSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender; // Assigning the object "sender" as Entry

            try
            {
                inputSize.TextChanged -= inputSize_TextChanged; // This removes this TextChanged Event for this Entry

                // Then checks wether the length of the entry is greater than 0
                generateButton.IsEnabled = inputSize.Text.Length > 0; 
                // And if it is greater than 0, it enables the Generate Button
            }
            finally
            {
                // Then When the Button is Enabled, This TextChanged event will be added again to this Entry
                inputSize.TextChanged += inputSize_TextChanged; 
            }
        }

        #region Button OnClick Events

        // This is OnClick event of the copy button to actually copy the generated password
        private async void copyButton_Clicked(object sender, EventArgs e)
        {
            // Making sure that the label displaying the generted password is not empty
            if (!string.IsNullOrEmpty(displayPassword.Text))
            {
                // If the label is the empty then copying the Text
                await Clipboard.SetTextAsync(displayPassword.Text);
            }
        }

        // This is OnClick Event of generate button
        private async void generateButton_Clicked(object sender, EventArgs e)
        {
            // Checking if the user did not check any of the checkbox
            if (!upper && !lower && !special && !number && !empty)
            {
                //If not then the user is asked to check atleast anyone of the checkbox
                // If the user presses the OK button in the DisplayAlert this statement will return true
                // If the user pressed the Cancel button then this will return false
                // Assigning the boolean value of the DisplayAlert directly to anyone of the CheckBox
                upperCheck.IsChecked = await DisplayAlert("Error !", "Please include any one of these.", "OK", "Cancel");
                // Returning from this is the most important thing as if the user selected Cancel button then the
                // code will continue to execute wich will pop out an exception
                // So to prevent tht exception return is mandatory
                return;
            }

            try
            {
                // Trying to Parse the value of the Password Length Entry and then assigning it to a variable of type integer
                passwordLength = int.Parse(inputSize.Text);
            }
            catch (ArgumentNullException)
            {
                //If the user left the Entry blank this expection is fired, to handle this,
                // We are displaying an alert
                await DisplayAlert("Error", "Please give some length to your password", "OK");
            }
            catch (FormatException)
            {
                // This usually pops out when the user types decimal or string values as those two are not integers
                await DisplayAlert("Error", "Please type only whole numbers not decimals", "OK");
            }
            catch (OverflowException)
            {
                // This is when the user types a number greater than 2147483647
                await DisplayAlert("Error", "Please give length below " + int.MaxValue.ToString(), "OK");
            }
            finally
            {
                //Then finally if the try block succesfully executed then these code will be executed
                // This If Else statement is purely optional depending upon the developer
                // Wether to force the user to create a password of minimum length of 6 
                if (passwordLength <= 5)
                {
                    await DisplayAlert("Error !", "Please give  length a minimum of 6", "OK");
                }
                else
                {
                    // This is the most important thing as these are the lines to create passwords
                    // This acess the mthod from another namespace which we have created and passing it all the bool value of checkboxes and the passwordLength
                    // And saving the generated password to a string variable
                    generatedPassword = PasswordGenerator.GeneratePassword(upper, lower, number, special, empty, passwordLength);
                    // After generating and saving temporarily
                    // displaying the text of Label as this generated password variable
                    displayPassword.Text = generatedPassword;
                    // This is purely optional and the use of this is to remove the valu of generatedPassword string
                    // When removed this will free up some space of you RAM where your temporary variables will be saved
                    // So, this improves the performance to the maximum level by reducing the RAM consumption
                    generatedPassword.Remove(0);
                    // Enabling the copy button
                    copyButton.IsEnabled = true;                
                }
            }
        }
        #endregion Button OnClick Events


        // Getting the value of the Checkbox to boolean from OnCheckChanged Event

        #region Check Changed Events
        private void upperCheck_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            upper = e.Value;
        }

        private void lowerCheck_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            lower = e.Value;
        }

        private void numberCheck_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            number = e.Value;
        }

        private void specialCheck_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            special = e.Value;
        }

        private void emptyCheck_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            empty = e.Value;
        }
        #endregion Check Changed Events
    }
}
