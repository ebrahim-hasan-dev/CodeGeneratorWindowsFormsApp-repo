# C# Four-Tier WinForms Code Generator (Initial Version)

A professional, high-productivity tool designed to automate the generation of clean, scalable, and standardized code for WinForms applications in terms of presentation layer. This generator strictly adheres to the **Four-Tier Architecture** pattern, significantly reducing development time by automating boilerplate code for the UI, Business, Modules, and Data Access layers.

---

## 🏗 Architecture & Dependency Flow

The project is built on a strict Four-Tier Architecture (N-Tier) to ensure separation of concerns and maintainability. The generated code and the tool itself follow this dependency flow:

1.  **Presentation Layer (PL):** Windows Forms App (.NET Framework).
    *   *Depends on:* Business Layer & Modules Layer.
2.  **Business Layer (BAL):** Class Library (.NET Framework).
    *   *Depends on:* Data Access Layer & Modules Layer.
3.  **Data Access Layer (DAL):** Class Library (.NET Framework).
    *   *Depends on:* Modules Layer.
4.  **Modules Layer:** Class Library (.NET Framework).
    *   *Depends on:* Nothing (Contains POCO classes and Entities).

---

## 🚀 Key Features

### 1. Logic & Data Layer Generation
*   **Database Integration:** Seamlessly connects to SQL Server using `Server Name`, `User ID`, and `Password`.
*   **Metadata Processing:** Fetches all databases, tables, views and columns using `INFORMATION_SCHEMA`.
*   **Customizable CRUD Operations:** Choose to generate specific methods like `IsExist`, `IsFind`, `Add`, `Update`, `GetAll`, and `Delete`.
*   **Smart Parameter Mapping:** Automatically identifies Primary Keys as parameters. Users can manually set any column as a parameter via a **Right-Click** context menu.
*   **Incremental Code Injection (Strongest Feature):** If a file already exists, the tool **appends** new methods instead of overwriting the file, preserving your manual modifications.
*   **Bulk Generation:** Ability to generate classes for all tables/views in the database with a single click.

### 2. Presentation Layer Generation
*   **Dynamic UI Mapping:** Automatically creates WinForms controls based on the SQL Data Type of each column.
*   **Flexible Components:** Generate either a standard **Form** or a **User Control**.
*   **UI Modes:**
    *   **Display Only:** For viewing data.
    *   **Data Entry:** For inputting new records.
    *   **Combined:** For both viewing and editing.
    *   **DataGrid View:** Includes a grid with automated filtering using `ComboBox`, `MaskedTextBox`, and search button.
*   **Manual Overrides:** Change control types manually or exclude specific columns from being generated.
*   **Naming Conventions:** Allows renaming the table/class. Underscores in column names are automatically handled for cleaner UI labels.

---

## 🛠 Tech Stack

*   **Language:** C# (.NET Framework)
*   **Architecture:** Four-Tier (N-Tier)
*   **Database:** SQL Server & ADO.NET
*   **UI Framework:** WinForms
*   **Template Engine:** T4 Runtime Text Templates (RunTime Text Template)
*   **Development Tools:** Visual Studio 2022, SSMS

---

## 📖 How it Works

### I. Generating DAL, BAL, and Modules
1. **Connect:** Enter your Server Name, User ID, and Password.
2. **Select:** Choose your Database and target Table/View.
3. **Configure:**
    * Select the required CRUD methods.
    * Rename the Table/Class if necessary.
    * Set custom parameters via Right-Click on columns.
4. **Pathing:** Provide the project directory and the Modules namespace.
5. **Build:** Click generate. The tool creates .cs files or appends logic to existing ones.
6. **Select:** Choose to generate for a single table or the entire database.


### II. Generating the Presentation Layer
1. **Open PL Designer:** Right-click on a table to open the UI Generator.
2. **UI Config:** Select the UI type (Form/User Control) and Mode (Input/Output/Both/View).
3. **Control Mapping:** Customize the controls in the DataGrid (change control type, visibility, or name).
4. **Output:** Set the output path. The tool generates three files: `.cs`, `.Designer.cs`, and `.resx`.

> [!IMPORTANT]  
> **Integration Tip:** When adding the generated UI to your project, only add the base class file, which ends with the `.cs` extension. Visual Studio will automatically link the `.Designer.cs` and `.resx` files as long as they are in the same directory.

---

## 💎 Why This Tool?

*   **Architectural Integrity:** Forces a clean separation between layers, making apps easier to maintain.
*   **Customizable:** Unlike generic generators, this allows for manual control over parameters and UI elements.
*   **Non-Destructive:** The "Append" feature ensures you never lose your manual code logic when adding new database functions.
*   **Speed:** Reduces days of repetitive coding into minutes, and accelerates productivity.
*   **Consistency:** Ensures the same architectural patterns are followed across the entire project.
*   **Error Reduction:** Eliminates manual typing errors in SQL queries and parameter mapping.
*   **Developer Friendly:** Specifically optimized for WinForms developers who require high-performance desktop applications with a clean N-Tier structure.

---

## 👤 About the Author

**Ebrahim Hasan**
A passionate Software Developer with a solid foundation in Computer Science. My journey started with learning the basics of the C++ language (Functional programming), then mastering **Algorithms, Object Oriented Programming and Data Structures** in C++, then learning C#, .NET and SQL Server Database, which paved the way for building complex systems using **C# and .NET**.

* **Expertise:** Desktop Applications (WinForms), SQL Server Database Design, and Logic Automation.
* **Key Projects:** Driver License Management System, Code Generator Tool.
* **Current Goal:** Transitioning into Web Full-stack Development (C#/.NET Backend).

---
📫 **Connect with me:**
* 📧 **Email:** [ebrahim.hasan.dev@gmail.com](mailto:ebrahim.hasan.dev@gmail.com)
* 💼 **LinkedIn:** [Your Profile Name](https://linkedin.com/in/ebrahim-hasan-dev)
