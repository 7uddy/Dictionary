# Dictionary

<h3>Description</h3>
This explanatory dictionary application is developed using the C# programming language and WPF (part of the .NET framework). Its purpose is to provide users with a platform for managing and searching for words in an intuitive and efficient manner.<br><br>
<h3>Modules</h3>
The dictionary contains three main modules.<br><br>

- **Administrative Module**
  - Allows adding, modifying, and deleting words from the dictionary.
  - Each word has an associated description, a mandatory category, and an optional image.
  - Images are selected from the computer and stored in a resource area of the application.
  - Validations are applied to the fields in the administration form.
  - Words and related information are saved in a text file.

- **Word Search Module**
  - Users can search for words either from a specific category (selected by them) or without considering the category.
  - Autocompletion is available in a textbox for entering searched words.
  - After selecting a word from the results list, the description, category, and image (or a default image) are displayed.

- **Entertainment Module**
  - Includes a game where users have to guess 5 words from the dictionary.
  - For each word, either the description or the image is displayed (randomly).
  - Users receive feedback on the correctness of their answers and can move on to the next and previous word.
