# unity-programming-theory
Unity Jr. Programmer course submission.

This repo is from my study of the Unity Jr. Programmer course. It's meant to demonstrate an understanding of object oriented programming principles. 
All of the programming occurs in the ./Programming-Theory-Project/Assets/Scripts/ directory, if you're interested in reviewing the scripts.

The game is a demonstration of natural principles regarding how populations change with available resources.
Carrots grow over time and seed more carrots if they aren't eaten. 
Rabbits eat carrots and reproduce. 
Wolves eat Rabbits and reproduce. 
Dead animals return their calories to the soil and grow more carrots.

Basic OOP class inheritance overview:

Organism ---> Plants ---> Carrots
          \-> Animals ---> Rabbits
                      \--> Wolves
                      
Plants and Animals inherit methods like "Die()" and "Age()" (lovely, I know) from Organism. 
Wolf classes and Rabbit classes inherit every method from the Animal class and polymorph through differed variable values.

It was pretty fun to write and took me about 2 days.
