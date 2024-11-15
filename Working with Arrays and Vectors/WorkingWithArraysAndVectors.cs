#include "splashkit.h" // Include the splashkit library for I/O and utilities

using namespace std; // Use the standard namespace

// Function to read a string input from the user
string read_string(string prompt)
{
    string result;
    write(prompt); // Display the prompt
    result = read_line(); // Read input line from user
    return result; // Return the user's input
}

// Function to read an integer input from the user
int read_integer(string prompt)
{
    string result;
    write(prompt); // Display the prompt
    result = read_line(); // Read input line from user
    return convert_to_integer(result); // Convert the input to an integer and return
}

// Function to read a double input from the user
double read_double(string prompt)
{
    string result;
    write(prompt); // Display the prompt
    result = read_line(); // Read input line from user
    return convert_to_double(result); // Convert the input to a double and return
}

// Function to calculate the total length of all names in an array
int total_length(string names[], int size)
{
    int result = 0;

    for (int i = 0; i < size; i++) // Loop through each name in the array
    {
        string name = names[i];
        result += name.length(); // Add the length of each name to the total
    }
    return result; // Return the total length
}

// Function to check if an array contains a specific name
bool contains(string names[], int size, string name)
{
    for (int i = 0; i < size; i++) // Loop through each name in the array
    {
        if (to_lowercase(names[i]) == to_lowercase(name)) // Case-insensitive check
        {
            return true; // Return true if the name is found
        }
    }
    return false; // Return false if the name is not found
}

// Function to find the longest name in an array
string longest_name(string names[], int size)
{
    string max = names[0]; // Assume the first name is the longest initially

    for (int i = 0; i < size; i++) // Loop through each name
    {
        if (max.length() < names[i].length()) // Check if current name is longer
        {
            max = names[i]; // Update max if a longer name is found
        }
    }
    return max; // Return the longest name
}

// Function to find the shortest name in an array
string shortest_name(string names[], int size)
{
    string min = names[0]; // Assume the first name is the shortest initially

    for (int i = 1; i < size; i++) // Loop through each name starting from the second
    {
        if (min.length() > names[i].length()) // Check if current name is shorter
        {
            min = names[i]; // Update min if a shorter name is found
        }
    }
    return min; // Return the shortest name
}

// Function to get the index of a specific name in an array
int index_of(string names[], string name, int size)
{
    for (int i = 0; i < size; i++) // Loop through each name
    {
        if (to_lowercase(name) == to_lowercase(names[i])) // Case-insensitive comparison
        {
            return i; // Return the index if the name is found
        }
    }
    return -1; // Return -1 if the name is not found
}

// Function to allow the user to change a name in the array
void change_name(string names[], int size)
{
    string name = read_string("Please enter something in the search: ");
    for (int i = 0; i < size; i++) // Loop through each name
    {
        if (to_lowercase(names[i]) == to_lowercase(name)) // Case-insensitive check
        {
            string new_name = read_string("Please enter updated name: ");
            write_line(new_name);
            names[i] = new_name; // Update the name in the array
        }
    }
}

// Function to print a summary of the names array
void print_summary(string names[], string name, int size)
{
    int length = total_length(names, size); // Calculate the total length of all names
    string long_name = longest_name(names, size); // Find the longest name
    int index = index_of(names, "dyson", size); // Get the index of "dyson"

    write_line(length); // Print total length of all names
    write_line(long_name); // Print the longest name
    write_line(index); // Print the index of "dyson" or -1 if not found
}

int main()
{
    #define SIZE 3 // Define the size of the array

    string names[SIZE]; // Array to store names
    int i = 0;

    // Loop to read names into the array
    while (i < SIZE)
    {
        names[i] = read_string("Enter a name: "); // Prompt user to enter a name
        i++;
    }

    // Print each name in the array
    for (int i = 0; i < SIZE; i++)
    {
        write_line(names[i]); // Print the name
    }

    // Calculate and print the total length of all names
    int total = total_length(names, SIZE);
    write("Total length: ");
    write_line(total);

    // Check if "dyson" is in the array and print the result
    bool has_dyson = contains(names, SIZE, "dyson");
    if (has_dyson)
        write_line("Contains dyson");

    // Print the shortest and longest names
    write_line(shortest_name(names, SIZE));
    write_line(longest_name(names, SIZE));

    // Output details before and after a name change
    print_summary(names, "", SIZE);
    change_name(names, SIZE);
    print_summary(names, "", SIZE);

    write_line("Goodbye"); // Print goodbye message

    return 0;
}
