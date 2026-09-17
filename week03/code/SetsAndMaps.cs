using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        //Create an internal method to reverse the order of the string
        string ReverseWord(string word)
        {
            string reversed = "";
            for (int i = word.Length - 1; i >= 0; i--)
            {
                reversed += word[i];
            }
            return reversed;
        }

        //Create an empty set to add the matching words to
        var wordSet = new HashSet<string>();
        var wordPairs = new HashSet<string>();

        //Loop through each word in words and check if there is a reverse version in the set
        foreach (string word in words)
        {
            var reverse = ReverseWord(word);
            if (wordSet.Contains(reverse))
            {                
                wordPairs.Add($"{reverse} & {word}"); //reverse will actually be the first one of these two in the set since it won't get added until both are in the array.
            }

            wordSet.Add(word);            
        }

        return wordPairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            var degree = fields[3];

            //If the type of education/degree is not in the summary table yet, add it
            if (!degrees.ContainsKey(degree))
            {
                degrees[degree] = 1;
            } else
            {
                //If the type of education is in the table then update the value
                degrees[degree] += 1;
            }        
        }
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE
        //Make all uppercase and get rid of spaces since it says to ignore those things
        word1 = word1.ToUpper().Replace(" ", "");
        word2 = word2.ToUpper().Replace(" ", "");

        if (word1.Length == word2.Length)
        {
            //word 1
            var letters1 = new Dictionary<char, int>();

            for (int i = 0; i < word1.Length; i++)
            {
                var letter = word1[i];

                //Check if dictionary contains that letter
                if (letters1.ContainsKey(letter))
                {
                    //Increase count by 1 since it's already in the dictionary
                    letters1[letter] += 1;
                }
                else
                {
                    //Add to dictionary starting with a count of 1
                    letters1[letter] = 1;
                }
            }

            //word 2
            var letters2 = new Dictionary<char, int>();

            for (int j = 0; j < word2.Length; j++)
            {
                var letter = word2[j];

                //Check if dictionary contains that letter
                if (letters2.ContainsKey(letter))
                {
                    //Increase count by 1 since it's already in the dictionary
                    letters2[letter] += 1;
                }
                else
                {
                    //Add to dictionary starting with a count of 1
                    letters2[letter] = 1;
                }
            }

            //Go through and check that the number of each letter in the two dictionaries matches
            foreach (var key in letters1.Keys)
            {
                //If both dictionaries don't have that letter or if the number of each letter in the two dictionaries isn't the same, return false
                if (!letters2.ContainsKey(key) || letters1[key] != letters2[key])
                {
                    return false;
                }
            }

            //If it made it through the foreach loop without returning false it means that both dictionaries have the same number of each letter.
            return true;
        }
        
        //If the length of the two words (after removing spaces) isn't the same then they can't be anagrams.
        return false;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.
        return [];
    }
}