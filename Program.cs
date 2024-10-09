using System.Xml;

namespace _2048_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            ConsoleKeyInfo direction;
            string [,] game = { { "_", "2", "_", "2", "_", "_" },
                                { "_", "_", "4", "2", "_", "_" },
                                { "_", "2", "2", "_" , "_", "_"},
                                { "_", "4", "_", "4", "_", "_" },
                                { "_", "4", "_", "4", "_", "_" }
                              };

            do{                
                DrawGame(game, GetScore(game));
                Console.Write("Escolhe o movimento: ");
                direction = Console.ReadKey();
                Console.Write(direction.Key);

                CheckDirection(game, direction);
                
                DrawGame(game, GetScore(game));
            }
            while(!Game_Is_Over(game) && direction.Key != ConsoleKey.Escape);
        }

        public static void DrawGame(string[,] game, int score){
            Console.Clear();
            Console.WriteLine("\n");
            for(int i = 0; i < game.GetLength(0); i++){
                Console.Write("|");
                for(int j = 0; j < game.GetLength(1); j++){
                    Console.Write($" {game[i,j]} |");
                }
                Console.WriteLine();                
            }

            Console.WriteLine($"\nPontuação: {score}");            
        }

        public static void MoveRight(string[,] game){
            // Procura por numeros
            for(int line = 0; line < game.GetLength(0); line++){
                for(int col = game.GetLength(1)-1; col >= 0; col--){
                    if(!isEmpty(game, line, col)){
                        
                        //Verifica se o numero está nos limites do jogo/campo
                        if(col != game.GetLength(1)-1){
                            //Ciclo para o numero seguinte
                            for(int nextCol = col+1; nextCol < game.GetLength(1); nextCol++){
                                //Verifica se o proximo numero é "_" e substitui os valores
                                if(game[line, nextCol] == "_"){
                                    game[line, nextCol] = game[line, nextCol-1];
                                    game[line, nextCol-1] = "_";
                                }
                                //Verifica se o proximo numero é igual ao anterior, se for somam-se
                                else if(game[line,nextCol-1] == game[line, nextCol]){
                                    string newValue = (Convert.ToInt16(game[line, nextCol-1]) + Convert.ToInt16(game[line, nextCol])).ToString();
                                    game[line, nextCol] = newValue;
                                    game[line, nextCol-1] = "_";
                                }
                            }  
                        }                      
                    }                 
                }
            }
            createNewNumbers(game, "line", 0, 0);
        }

        public static void MoveLeft(string [,] game){
            // Procura por numeros
            for(int line = 0; line < game.GetLength(0); line++){
                for(int col = 0; col < game.GetLength(1); col++){
                    if(!isEmpty(game, line, col)){

                        //Verifica se o numero está na 1ª coluna
                        if(col != 0){  
                            for(int nextCol = col-1; nextCol >= 0; nextCol--){
                                //Verifica se o proximo numero é "_" e substitui os valores
                                if(game[line, nextCol] == "_"){
                                    game[line, nextCol] = game[line, nextCol+1];
                                    game[line, nextCol+1] = "_";                                    
                                } 
                                //Verifica se o numero é igual ao do lado direito, se for somam-se
                                else if(game[line, nextCol] == game[line, nextCol+1]){
                                string newValue = (Convert.ToInt16(game[line, nextCol]) + Convert.ToInt16(game[line, nextCol+1])).ToString();
                                game[line, nextCol] = newValue;
                                game[line, nextCol+1] = "_";
                                }
                            }   
                        }

                        else if(col == 0 && game[line, col] == game[line, col+1]){
                            string newValue = (Convert.ToInt16(game[line, col]) + Convert.ToInt16(game[line, col+1])).ToString();
                            game[line, col] = newValue;
                            game[line, col+1] = "_";
                        } 
                    }                   
                }
            }            
            createNewNumbers(game, "line", 0, game.GetLength(0));
        }
        public static void MoveUp(string [,] game){
            // Procura por numeros
            for(int line = 0; line < game.GetLength(0); line++){
                for(int col = 0; col < game.GetLength(1); col++){
                    if(!isEmpty(game, line, col)){
                        
                        //Verifica se o numero não está na 1ª linha
                        if(line != 0){
                            //Ciclo para o numero seguinte
                            for(int nextLine = line-1; nextLine >= 0; nextLine--){
                                //Verifica se o proximo numero está vazio e substitui os valores
                                if(game[nextLine, col] == "_"){
                                    game[nextLine, col] = game[nextLine+1, col];
                                    game[nextLine+1, col] = "_";                                    
                                }

                                //Verifica se o numero não está na ultima linha e se o proximo numero é igual ao anterior, 
                                //, se for somam-se
                                else if(game[nextLine, col] == game[nextLine+1, col]){
                                string newValue = (Convert.ToInt16(game[nextLine, col]) + Convert.ToInt16(game[nextLine+1, col])).ToString();
                                game[nextLine, col] = newValue;
                                game[nextLine+1, col] = "_";
                                }
                                else { continue; }                              
                            }                               
                        }                 
                    }
                }
            }
            
            createNewNumbers(game, "column", 1, game.GetLength(0)-1);
        }
        public static void MoveDown(string [,] game){
            // Procura por numeros
            for(int line = game.GetLength(0)-1; line >= 0; line--){
                for(int col = 0; col < game.GetLength(1); col++){
                    if(!isEmpty(game, line, col)){
                        
                        //Verifica se o numero não está na ultima linha
                        if(line != game.GetLength(0)-1){
                            //Verifica se o proximo numero é igual ao anterior, se for somam-se
                            if(game[line, col] == game[line+1, col]){
                                string newValue = (Convert.ToInt16(game[line, col]) + Convert.ToInt16(game[line+1, col])).ToString();
                                game[line+1, col] = newValue;
                                game[line, col] = "_";
                            }

                            for(int nextLine = line+1; nextLine < game.GetLength(0); nextLine++){
                                //Verifica se o proximo numero está vazio e substitui os valores
                                if(game[nextLine, col] == "_"){
                                    game[nextLine, col] = game[nextLine-1, col];
                                    game[nextLine-1,col] = "_";                                    
                                }                             
                            }
                        } 
                    }
                }
            }            
            createNewNumbers(game, "column", 1, 0);
        }
        public static void CheckDirection(string[,] game, ConsoleKeyInfo direction){
            if(direction.Key == ConsoleKey.RightArrow) MoveRight(game);
            if(direction.Key == ConsoleKey.LeftArrow) MoveLeft(game);
            if(direction.Key == ConsoleKey.UpArrow) MoveUp(game);
            if(direction.Key == ConsoleKey.DownArrow) MoveDown(game);
        }

        public static bool isEmpty(string [,] game, int line, int col){ 
            return game[line, col] == "_";
        }        
        public static void createNewNumbers(string[,] game, string location, int dimension, int limit){
            int[] newNumbers = [2, 4];
            double nr_newNumbers = Math.Truncate(game.GetLength(dimension)*0.4);
            var randomNumber = new Random();
            Random random = new Random();
            int randomLine;
            int freeLines = 0;
            
            if(location == "line"){
                for(int line = 0; line < game.GetLength(dimension); line++){
                    if(game[line, limit] == "_"){
                        freeLines++;
                    }
                }

                if(nr_newNumbers > freeLines) nr_newNumbers = freeLines;
                
                //Verifica o nº de numeros novos a colocar dependendo da quantidade de linhas e colunas
                //Para cada nº de numeros novos, escolhe aleatóriamente uma coluna/linha livre
                if(nr_newNumbers > 0){
                    for(int i = 0; i < nr_newNumbers; i++){
                        do{
                            randomLine = random.Next(0, game.GetLength(dimension));                                
                        }
                        while(game[randomLine, limit] != "_"); 

                        //Coloca um numero aleatório 2/4 na linha aleatória
                        game[randomLine, limit] = newNumbers[randomNumber.Next(0, 2)].ToString();  
                    }
                }  
            }
            else{
                for(int col = 0; col < game.GetLength(dimension); col++){
                    if(game[limit, col] == "_"){
                        freeLines++;
                    }
                }

                if(nr_newNumbers > freeLines) nr_newNumbers = freeLines;
                
                //Verifica o nº de numeros novos a colocar dependendo da quantidade de linhas e colunas
                //Para cada nº de numeros novos, escolhe aleatóriamente uma coluna/linha livre
                if(nr_newNumbers > 0){
                    for(int i = 0; i < nr_newNumbers; i++){
                        do{
                            randomLine = random.Next(0, game.GetLength(dimension));                                
                        }
                        while(game[limit, randomLine] != "_"); 

                        //Coloca um numero aleatório 2/4 na linha aleatória
                        game[limit, randomLine] = newNumbers[randomNumber.Next(0, 2)].ToString();  
                    }
                }
            }     
        }    
        public static bool Game_Is_Over(string[,] game){
            bool game_is_over = true;
            for(int line = 0; line < game.GetLength(0); line++){
                for(int col = 0; col < game.GetLength(1); col++){
                    if(game[line, col] == "_") game_is_over = false;
                }
            }
            if(game_is_over) Console.WriteLine("Jogo terminou. \n");
            
            return game_is_over;
        }
    
        public static int GetScore(string[,] game){
            int maxNum = 0;

            for(int line = 0; line < game.GetLength(0); line++){
                for(int col = 0; col < game.GetLength(1); col++){
                    if(game[line, col] != "_" && maxNum < Convert.ToInt16(game[line, col]))  
                        maxNum = Convert.ToInt16(game[line, col]);
                }
            }            
            return maxNum;
        }
    }
}
