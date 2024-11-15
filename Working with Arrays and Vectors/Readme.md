
# 📋 Name Summary Program

Welcome to the **Name Summary Program**! 🎉 This is a fun, interactive C++ program designed to help you play around with names. Imagine it as your personal name assistant—it takes in a list of names, analyzes them, and provides interesting information, like finding the longest or shortest name, the total number of characters in all names, and whether certain names are present in the list. Think of it like a super-simplified version of a data management system for names!

---

## 🛠 Features

Here’s what the program does for you:

1. **Get Name Inputs** ✍️  
   Enter any name you like, and the program will remember it. Just like filling out a guestbook but without a pen!

2. **Calculate Total Name Length** 📏  
   The program will add up the length of each name and tell you the total. For example, if you enter "Alice" and "Bob," it will say the total length is 8 characters (5 from "Alice" + 3 from "Bob").

3. **Find the Longest and Shortest Name** 📏🔍  
   Want to know which name is the longest? Or the shortest? This program will tell you! Handy for deciding who has the coolest or shortest name!

4. **Check for a Specific Name** 🔎  
   It can check if a specific name (like "Dyson") is in the list. If it’s there, the program will let you know! It’s like asking, "Is my best friend in the room?"

5. **Update a Name** ✏️  
   If you want to change any name, you can! Simply tell it the name you want to replace, and give the new name—it’s as easy as editing a name tag!

6. **Summary of All Names** 📊  
   The program can provide a quick summary of all names you've entered, including details like the longest name, the total name length, and if a specific name was found.

---

## 📂 How to Run the Program

### 1. Requirements
- **C++ Compiler**: Make sure you have a C++ compiler like `g++` installed.
- **SplashKit Library**: This program uses the **SplashKit** library for input/output functions, so be sure to install it.

### 2. Compiling and Running

Once you've saved the code in a file (e.g., `name_summary.cpp`), open a terminal, and compile it using:

```bash
g++ name_summary.cpp -o name_summary -I path/to/splashkit/headers -L path/to/splashkit/libs -lsplashkit
```

Then, run the compiled program:

```bash
./name_summary  # On macOS and Linux
name_summary.exe  # On Windows
```

---

## 🖥 Example Run

Let’s say you run the program and type in the names `"Alice"`, `"Bob"`, and `"Charlie"`. Here’s a sample interaction:

```
Enter a name: Alice
Enter a name: Bob
Enter a name: Charlie

Names Entered:
- Alice
- Bob
- Charlie

Total length of all names: 14
Longest name: Charlie
Shortest name: Bob
Contains "Dyson": No
```

After that, you can update any name in the list and get an updated summary!

---

## 📜 Explanation of Each Part

Here’s what each section of the code does:

- **Read String, Integer, and Double Inputs**: The program can read text (like names) and numbers. For example, it asks you to “Enter a name” and then stores that name for later use.
- **Name Analysis Functions**: There are functions to get the total length of names, find the longest and shortest names, and check if a specific name (like "Dyson") is present in the list.
- **Updating a Name**: You can choose to replace a name. For instance, if you originally entered "Bob," you can change it to "Bobby" using this feature.
- **Print Summary**: Outputs the results, including total length, longest and shortest names, and whether "Dyson" is in the list.

---

## 🧠 Why Use This Program?

Imagine you’re hosting an event and need a quick tool to manage guest names. This program can help you:

- Quickly list and review the longest and shortest names.
- Easily change someone’s name if they prefer a nickname.
- Check if your friend “Dyson” is on the guest list!

---

## 📈 Future Improvements

Here are some ideas for enhancing this program:

- **Add More Search Features**: Enable searching by the first letter of names or by length.
- **Add More Name Statistics**: Calculate the average name length or count names by length.
- **Support for More Data Types**: Add support for additional types of information like ages or locations.

---

Thank you for checking out the **Name Summary Program**! If you have any questions or need help, feel free to reach out. Happy coding! 👩‍💻👨‍💻
