                /////////////////////////////////////////////////////////   
                    // Vicky2004-maker/Xamarin_Password_Gen @ GitHub    
                                    // Apache License 2.0               
                                    // Developed by Vicky
                                    // Difficulty - Beginner
                /////////////////////////////////////////////////////////

using System;

namespace Password_Generator.Services // Use this nammespace in the page where you have to generate a password
{
    // Marking as static is also importatnt so that we don't need to create a instance of this to access this from another 
    public static class PasswordGenerator
    {
        // Mark these string as public so that you can access it from anywhere
        public static string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string number = "1234567890";
        public static string special = "!@#$%^&*()_+";

        public static string GeneratePassword(bool useUpper, bool useLower, bool useNumbers, bool useSpecial, bool useEmpty, int passwordSize)
        {
            char[] password = new char[passwordSize]; // Create a new char array of length of password size
            string charSet = ""; // Create a unassigned local string variable 
            Random random = new Random(); // Create a new Random

            // These if statements are used depending upon the checkbox checked
            if (useUpper) charSet += upper;
            if (useLower) charSet += upper.ToLower();
            if (useNumbers) charSet += number;
            if (useSpecial) charSet += special;
            if (useEmpty) charSet += " ";

            // Loop through every number of password length
            for (int i = 0; i < passwordSize; i++)
            {
                // For each number assigning the valu of char array as the random of integer selected from charset
                password[i] = charSet[random.Next(charSet.Length - 1)];
            }

            // As this is a string method this should return a string
            // So, this returns a string of array of char that we created
            return string.Join(null, password);
        }
    }
}
