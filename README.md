﻿# Cobra

> Solving [The Cobra effect](https://en.wikipedia.org/wiki/Cobra_effect) on the spot

[![Build Status](https://petarangelov15-gmail.visualstudio.com/Cobra/_apis/build/status/Shannarra.Cobra?branchName=master)](https://petarangelov15-gmail.visualstudio.com/Cobra/_build/latest?definitionId=1&branchName=master)

## Why?
The modern-day programming languages are __good__ but not __*the best*__.  
Or at least the ones before Cobra&copy;.

## Features
Cobra&copy; is my first real-life compiler with features from different programming languages around the world.

#### Current features
Keep in mind that this is still a _very_ young project, so some of the features will change with time.
<ol style="list-sty">
    <li>
        Integration  
        <p>The project is continuously integrated using <a href="">Azure Pipelines</a>, so the status badge will always indicate if something's wrong.</p>
    </li>
    <li>
        CLI
        <p>At least for now, the early stages of the compiler mean that every single compilation result and input are done through the CLI. This will change in the near future.</p>
    </li>
    <li>
        Operators
        <p>The operators in the language are <strong>strict</strong>, and <strong>left-biased</strong>. The only exception to that rule is the assignment operator (=), which binds to the right side of the expression.</p>
    </li>
    <li>
        Compiler commands
        <ul>
            <li>.printToggle
               <p>Prints the current AST to the console in the style of the <a href="https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/tree">windows "tree" command</a></p>
            </li>
            <li> .exit, .clear
                <p>Clears or exits the current compiler iteration.</p>
            </li>
            <li> .getVars
                <p>Prints the currently instantiated variables as key-value pairs.</p>
            </li>
        </ul>
    </li>
</ol>

### Examples
1. Variables  
   __For now__ variables are just created via identifier and value pair
    ```
    a = 10
    b = 5
    c = a * b // 50
    d = c * c // 2500
    ```
2. Expressions
    The CLI can also evaluate _all_ kinds of expressions
    ```
    // using the values of the previous example
    d - (c * b - a) //2260
    d && b // Binary operator && is not defined for types System.Int32 and System.Int32.
    ```
    ![Bin](https://github.com/Shannarra/Cobra/blob/master/docs/bin_operator_error.png?raw=true)  
3.Operators
    The operators can be C-styled as well as Python-styled 
    ``` 
    finished = true
    
    // unary operators
    !finished             // false
    
    //binary operators
    finished and true     // true
    finished or true      // true
    finished && true      // true
    finished || true      // true
    finished is true      // true
    finished == true      // true
    finished !is true     // false
    finished != true      // false
    finished is !false    // true
    finished == !false    // true
    finished != !false    // false
    finished !is !false   // false
    // and so much more...
    ```

### Future features
- File support
- Strong type system
- Better expression evaluation