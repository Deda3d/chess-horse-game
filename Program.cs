using static System.Console;

SetWindowSize(30, 15);
SetBufferSize(30, 15);

//робимо поле 12 на 12, у якому 0 - пустий простір, 9 - бортики, 1 - місця, на яких побував кінь. сам кінь - 9 
int[,] GameField = new int[12, 12];

for (int i = 0; i < 12; i++)
{
    for (int j = 0; j < 12; j++) GameField[i, j] = 9;
}
for (int i = 2; i < 10; i++)
{
    for (int j = 2; j < 10; j++) GameField[i, j] = 0;
}
GameField[2, 9] = 1; // одразу заповнюємо, бо тут кінь стоїть за замовчуванням
//

int xhorse = 9;
int yhorse = 2;
int score = 1;
int maxscore = 1;

bool wpressed = false; // змінна для того, щоб запам'ятати статус кнопки
bool apressed = false;
bool spressed = false;
bool dpressed = false;


void FieldPrint() // оновлення поля гри, щоб після зміни даних у ньому їх можна було побачити
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Use w,a,s,d to move");
    Console.WriteLine();
    for (int i = 2; i < 10; i++)
    {
        for (int j = 2; j < 10; j++)
        {
            if (i == yhorse && j == xhorse) Console.ForegroundColor = ConsoleColor.DarkGreen;
            else if (GameField[i, j] == 1 && i % 2 == j % 2) Console.ForegroundColor = ConsoleColor.DarkRed;
            else if (GameField[i, j] == 1 && i % 2 != j % 2) Console.ForegroundColor = ConsoleColor.Red;
            else if (i % 2 == j % 2) Console.ForegroundColor = ConsoleColor.DarkGray;
            else Console.ForegroundColor = ConsoleColor.White;
            if ((i == yhorse + 2 && j == xhorse && (spressed == true)) || // для того, щоб було видно напрямок, коли натискаємо w/a/s/d
                (i == yhorse - 2 && j == xhorse && (wpressed == true)) ||
                (i == yhorse && j == xhorse - 2 && (apressed == true)) ||
                (i == yhorse && j == xhorse + 2 && (dpressed == true)))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("┼┼");
            }
            else Console.Write("██");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine($"{64 - score} left");
    Console.WriteLine($"Your max score is {maxscore}");
}
FieldPrint();


bool game = true;
while (game == true)
{
    CursorVisible = false; // відключаємо курсор у консолі
    var key1 = Console.ReadKey(true).Key; // зчитуємо перше натискання кнопки на клавіатурі
    if (key1 == ConsoleKey.S)
    {
        spressed = true;
        FieldPrint(); // оновлюємо, щоб на полі було видно, що ми нажали цю кнопку
        var key2 = Console.ReadKey(true).Key; // зчитуємо друге натискання кнопки на клавіатурі
        if (key2 == ConsoleKey.A)
        {
            if (GameField[yhorse + 2, xhorse - 1] != 9 && GameField[yhorse + 2, xhorse - 1] != 1) // якщо ми не виходимо за поле (9) та якщо тут ще не стояв кінь (1)
            {
                Task.Run(() => Console.Beep(600, 200)); // програвання звуку при пересуванні
                yhorse += 2; // координати коня змінюються
                xhorse--;
                GameField[yhorse, xhorse] = 1; // додаємо інформацію на поле про те, що тут вже стояв кінь
                score++; // збільшуємо статистику наших пересувань на 1
                FieldPrint();
            }
            else Task.Run(() => Console.Beep(400, 200)); // якщо виходимо за поле або тут був кінь, програється інший звук
        }
        if (key2 == ConsoleKey.D)
        {
            if (GameField[yhorse + 2, xhorse + 1] != 9 && GameField[yhorse + 2, xhorse + 1] != 1)
            {
                Task.Run(() => Console.Beep(600, 200));
                yhorse += 2;
                xhorse++;
                GameField[yhorse, xhorse] = 1;
                score++;
                FieldPrint();
            }
            else Task.Run(() => Console.Beep(400, 200));
        }
        spressed = false; // очищуємо статус кнопки та оновлюємо поле
        FieldPrint();
    }

    if (key1 == ConsoleKey.A)
    {
        apressed = true;
        FieldPrint();
        var key2 = Console.ReadKey(true).Key;
        if (key2 == ConsoleKey.S)
        {
            if (GameField[yhorse + 1, xhorse - 2] != 9 && GameField[yhorse + 1, xhorse - 2] != 1)
            {
                Task.Run(() => Console.Beep(600, 200));
                yhorse++;
                xhorse -= 2;
                GameField[yhorse, xhorse] = 1;
                score++;
                FieldPrint();
            }
            else Task.Run(() => Console.Beep(400, 200));
        }
        if (key2 == ConsoleKey.W)
        {
            if (GameField[yhorse - 1, xhorse - 2] != 9 && GameField[yhorse - 1, xhorse - 2] != 1)
            {
                Task.Run(() => Console.Beep(600, 200));
                yhorse--;
                xhorse -= 2;
                GameField[yhorse, xhorse] = 1;
                score++;
                FieldPrint();
            }
            else Task.Run(() => Console.Beep(400, 200));
        }
        apressed = false;
        FieldPrint();
    }
    if (key1 == ConsoleKey.W)
    {
        wpressed = true;
        FieldPrint();
        var key2 = Console.ReadKey(true).Key;
        if (key2 == ConsoleKey.A)
        {
            if (GameField[yhorse - 2, xhorse - 1] != 9 && GameField[yhorse - 2, xhorse - 1] != 1)
            {
                Task.Run(() => Console.Beep(600, 200));
                yhorse -= 2;
                xhorse--;
                GameField[yhorse, xhorse] = 1;
                score++;
                FieldPrint();
            }
            else Task.Run(() => Console.Beep(400, 200));
        }
        if (key2 == ConsoleKey.D)
        {
            if (GameField[yhorse - 2, xhorse + 1] != 9 && GameField[yhorse - 2, xhorse + 1] != 1)
            {
                Task.Run(() => Console.Beep(600, 200));
                yhorse -= 2;
                xhorse++;
                GameField[yhorse, xhorse] = 1;
                score++;
                FieldPrint();
            }
            else Task.Run(() => Console.Beep(400, 200));
        }
        wpressed = false;
        FieldPrint();
    }
    if (key1 == ConsoleKey.D)
    {
        dpressed = true;
        FieldPrint();
        var key2 = Console.ReadKey(true).Key;
        if (key2 == ConsoleKey.W)
        {
            if (GameField[yhorse - 1, xhorse + 2] != 9 && GameField[yhorse - 1, xhorse + 2] != 1)
            {
                Task.Run(() => Console.Beep(600, 200));
                yhorse--;
                xhorse += 2;
                GameField[yhorse, xhorse] = 1;
                score++;
                FieldPrint();
            }
            else Task.Run(() => Console.Beep(400, 200));
        }
        if (key2 == ConsoleKey.S)
        {
            if (GameField[yhorse + 1, xhorse + 2] != 9 && GameField[yhorse + 1, xhorse + 2] != 1)
            {
                Task.Run(() => Console.Beep(600, 200));
                yhorse++;
                xhorse += 2;
                GameField[yhorse, xhorse] = 1;
                score++;
                FieldPrint();
            }
            else Task.Run(() => Console.Beep(400, 200));
        }
        dpressed = false;
        FieldPrint();
    }
    // перевіряємо, чи може наш кінь пересуватись, чи на його шляху є тільки (9) або (1). якщо ні - поразка
    if ((GameField[yhorse + 2, xhorse + 1] == 1 || GameField[yhorse + 2, xhorse + 1] == 9) &&
        (GameField[yhorse + 2, xhorse - 1] == 1 || GameField[yhorse + 2, xhorse - 1] == 9) &&
        (GameField[yhorse - 2, xhorse - 1] == 1 || GameField[yhorse - 2, xhorse - 1] == 9) &&
        (GameField[yhorse - 2, xhorse + 1] == 1 || GameField[yhorse - 2, xhorse + 1] == 9) &&

        (GameField[yhorse - 1, xhorse + 2] == 1 || GameField[yhorse - 1, xhorse + 2] == 9) &&
        (GameField[yhorse - 1, xhorse - 2] == 1 || GameField[yhorse - 1, xhorse - 2] == 9) &&
        (GameField[yhorse + 1, xhorse - 2] == 1 || GameField[yhorse + 1, xhorse - 2] == 9) &&
        (GameField[yhorse + 1, xhorse + 2] == 1 || GameField[yhorse + 1, xhorse + 2] == 9))
    {
        FieldPrint();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("YOU LOSE!!!!!!!!!!");
        Console.Beep(1319, 50);
        Console.Beep(1244, 50);
        Console.Beep(1175, 50);

        Console.ForegroundColor = ConsoleColor.White;
        game = false;
        if (score > maxscore) maxscore = score; // якщо це рекорд - записуємо

        Console.WriteLine();
        Console.Write("Try again?(yes/no): ");
        CursorVisible = true; // включаємо курсор, щоб ми змогли вписати відповідь на запитання
        string answer = Console.ReadLine();
        if (answer == "yes")
        {
            game = true; // відміняємо закінчення гри, щоб while було true
            for (int i = 2; i < 10; i++) // обнуляємо старе поле
            {
                for (int j = 2; j < 10; j++) GameField[i, j] = 0;
            }
            xhorse = 9; // знову відправляємо на початкові координати
            yhorse = 2;
            GameField[yhorse, xhorse] = 1;
            score = 1; // оновлюємо наш рахунок
            FieldPrint();

        }

    }
    //

    // якщо ми зробили 63 кроки, то це означає, що ми змогли пройти все поле, тому це перемога
    if (score == 64)
    {
        FieldPrint();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("YOU WIN!!!!!!!!!!");
        Console.Beep(1047, 100);
        Console.Beep(1318, 100);
        Console.Beep(1568, 100);
        Console.ForegroundColor = ConsoleColor.White;
        game = false;
        if (score > maxscore) maxscore = score;


        Console.WriteLine();
        Console.Write("Try again?(yes/no): ");
        CursorVisible = true;
        string answer = Console.ReadLine();
        if (answer == "yes")
        {
            game = true;
            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++) GameField[i, j] = 0;
            }
            xhorse = 9;
            yhorse = 2;
            GameField[yhorse, xhorse] = 1;
            score = 1;
            FieldPrint();

        }
    }
}