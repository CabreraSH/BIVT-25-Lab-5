using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab5
{
    public class Purple
    {
        public int[] Task1(int[,] matrix)
        {
            int[] answer = null;

            //===========================================================================================================================================================
            // В работе разрешены методы классов: Console, Math. Random, Convert, Array.

            // Не использовать больше двух уровней вложенности циклов

            // If you cannot make any changes to the matrix due to invalid input parameters
            // (for example, needing a square matrix but receiving a non-square one as input)
            // Return these default values:
                // For value types (int, double, etc): 0
                // For reference types: null
                // If the task expects an array of results, return an empty array

            // If your denominator is 0 when calculating an average, consider it to be 0

            // If you end up 'cleaning' an array (or can't find suitable values), return an empty array

            // When moving or sorting elements, maintain their relative order
            // When dealing with jagged arrays, check the length of each row when accessing an element
            // When creating a new square matrix, read the elements row by row from left to right and fill the missing elements with zeros in the last row

            //===========================================================================================================================================================

            // code here
            // PROBLEM ONE----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается матрица (двумерный массив) matrix. Сформировать одномерный массив из количеств отрицательных элементов столбцов матрицы.

            // Create a one-dimensional array of the negative column elements of the two-dimensional array (i.e. a matrix) "matrix"

            //---------------------------------------------------------------------------------------------------------------------------------------------

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            answer = new int[cols];

            for (int j = 0; j < cols; j++)
            {
                int count = 0;
                for (int i = 0; i < rows; i++)
                {
                    if (matrix[i, j] < 0)
                    {
                        count++;
                    }
                }

                answer[j] = count;
            }


            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

            return answer;
        }
        public void Task2(int[,] matrix)
        {

            // code here
            // PROBLEM TWO----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается матрица (двумерный массив) matrix.
            // В каждой строке матрицы минимальный элемент поместить в начало строки, сохранив порядок остальных элементов.
            // Если в строке несколько равных минимумов, выбирать первый(левый) и перемещать его в начало, остальные сохраняются в порядке.

            // In each row (строке) of the matrix, move the minimum element to the beginning of the row while preserving the order of the elements
            // If a row contains several (equal) minimum values, select the first one (the leftmost one) and move it to the beginning, while preserving order

            //------------------------------------------------------------------------------------------------------------------------------------------------

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (cols == 0)
            {
                return;
            }

            for (int i = 0; i < rows; i++)
            {
                int minIndex = 0;
                int minValue = matrix[i, 0];

                for (int j = 1; j < cols; j++)
                {
                    if (matrix[i, j] < minValue)
                    {
                        minValue = matrix[i, j];
                        minIndex = j;
                    }
                }

                int temp = matrix[i, minIndex];

                for (int j = minIndex; j > 0; j--)
                {
                    matrix[i, j] = matrix[i, j - 1];
                }

                matrix[i, 0] = temp;
            }


            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

        }
        public int[,] Task3(int[,] matrix)
        {
            int[,] answer = null;

            // code here
            // PROBLEM THREE----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается матрица (двумерный массив) matrix.
            // В каждой строке продублировать максимальный элемент, вставив новый элемент равный максимальному, сразу после максимального.
            // При нескольких максимумов брать первый(левый) максимум и вставлять новый элемент сразу после него (то есть сдвинуть последующие элементы вправо).

            // In each row, duplicate the maximum element by inserting a new element equal to the maximum inmediatly after it
            // If a row contains multiple maximum values, just work with the first one

            //---------------------------------------------------------------------------------------------------------------------------------------------------

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (cols == 0)
            {
                answer = new int[rows, 0];
                return answer;
            }

            answer = new int[rows, cols + 1];

            for (int i = 0; i < rows; i++)
            {
                int maxIndex = 0;
                int maxValue = matrix[i, 0];

                for (int j = 1; j < cols; j++)
                {
                    if (matrix[i, j] > maxValue)
                    {
                        maxValue = matrix[i, j];
                        maxIndex = j;
                    }
                }

                int newColumnIndex = 0;

                for (int j = 0; j < cols; j++)
                {
                    answer[i, newColumnIndex] = matrix[i, j];

                    if (j == maxIndex)
                    {
                        newColumnIndex++;
                        answer[i, newColumnIndex] = matrix[i, j];
                    }

                    newColumnIndex++;
                }
            }


            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

            return answer;
        }
        public void Task4(int[,] matrix)
        {

            // code here
            // PROBLEM FOUR----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается матрица (двумерный массив) matrix.
            // В каждой строке заменить все отрицательные элементы, расположенные перед первым (левым) максимальным элементом, на среднее арифметическое среди положительных элементов, расположенных после него.
            // Если после максимального нет положительных элементов, изменения не производить.
            // Для целочисленных матриц брать только целую часть среднего.

            // In each row, replace all negative elements that appear before the first maximum element, with the arithmetc mean of the positive elements located after that maximum
            // If there are no positive elements after the maximum, do not make any changes.
            // For integer matrices, use only the integer part of the mean

            //---------------------------------------------------------------------------------------------------------------------------------------------

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (cols == 0)
            {
                return;
            }

            for (int i = 0; i < rows; i++)
            {
                int maxValue = matrix[i, 0];
                int maxIndex = 0;

                for (int j = 1; j < cols; j++)
                {
                    if (matrix[i, j] > maxValue)
                    {
                        maxValue = matrix[i, j];
                        maxIndex = j;
                    }
                }

                int sumPos = 0;
                int countPos = 0;

                for (int j = maxIndex + 1; j < cols; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        sumPos += matrix[i, j];
                        countPos++;
                    }
                }

                if (countPos == 0)
                {
                    continue;
                }

                int avg = sumPos / countPos;

                for (int j = 0; j < maxIndex; j++)
                {
                    if (matrix[i, j] < 0)
                    {
                        matrix[i, j] = avg;
                    }
                }
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

        }
        public void Task5(int[,] matrix, int k)
        {

            // code here
            // PROBLEM FIVE----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается матрица (двумерный массив) matrix и индекс столбца (column) k.
            // Заменить k-й столбец матрицы одномерным массивом, состоящим из максимальных элементов строк,
            // расположенных в обратном порядке (т.е 1 - й элемент k - го столбца – это максимальный элемент последней строки).

            // Replace the k-th column of the matrix with a one-dimensional array made up of the maximum element of the rows,
            // arranged in reverse order (i.e., the first element of the k-th column is the maximum element of the last row

            //---------------------------------------------------------------------------------------------------------------------------------------------

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (rows == 0 || cols == 0)
            {
                return;
            }

            if (k < 0 || k >= cols)
            {
                return;
            }

            int[] rowMax = new int[rows];

            for (int i = 0; i < rows; i++)
            {
                int maxValue = matrix[i, 0];

                for (int j = 1; j < cols; j++)
                {
                    if (matrix[i, j] > maxValue)
                    {
                        maxValue = matrix[i, j];
                    }
                }

                rowMax[i] = maxValue;
            }

            for (int i = 0; i < rows; i++)
            {
                matrix[i, k] = rowMax[rows - 1 - i];
            }
            

            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

        }
        public void Task6(int[,] matrix, int[] array)
        {

            // code here
            // PROBLEM SIX----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается матрица (двумерный массив) matrix и одномерный массив array.
            // Заменить первый (верхний) максимальный элемент столбца соответствующим ему элементом массива array,
            // если этот элемент больше найденного максимального элемента столбца.

            // Replace the first (topmost) maximum element of the column with the corresponding element of the array,
            // if that element is greater than the maximum element found in the column.

            //---------------------------------------------------------------------------------------------------------------------------------------------

            if (array.Length != matrix.GetLength(1))
                return;

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                int max = Int32.MinValue, indexMax = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (max < matrix[i, j])
                    {
                        max = matrix[i, j];
                        indexMax = i;
                    }
                }

                if (array[j] > max)
                    matrix[indexMax, j] = array[j];
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

        }
        public void Task7(int[,] matrix)
        {

            // code here
            // PROBLEM SEVEN----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается матрица (двумерный массив) matrix.
            // Упорядочить строки по убыванию их минимальных элементов

            // Sort the rows in descending order according to their minimum elements

            //---------------------------------------------------------------------------------------------------------------------------------------------

            int[] minValue = new int[matrix.GetLength(0)];
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int min = Int32.MaxValue;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (min > matrix[i, j])
                        min = matrix[i, j];
                }
                minValue[i] = min;
            }

            int[] index = new int[minValue.Length];
            for (int i = 0; i < minValue.Length; i++)
            {
                index[i] = i;
            }

            // This particular for is used to Bubble Sort the array
            for (int i = 0; i < minValue.Length; i++)
            {
                for (int j = 1; j < minValue.Length - i; j++)
                {
                    if (minValue[j - 1] < minValue[j])
                    {
                        (minValue[j], minValue[j - 1]) = (minValue[j - 1], minValue[j]);
                        (index[j], index[j - 1]) = (index[j - 1], index[j]);
                    }
                }
            }

            int[,] clone = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    clone[i, j] = matrix[i, j];
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = clone[index[i], j];
                }
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

        }
        public int[] Task8(int[,] matrix)
        {
            int[] answer = null;

            // code here
            // PROBLEM EIGHT----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается квадратная матрица (двумерный массив) matrix.
            // Просуммировать элементы, расположенные на диагоналях, параллельных главной, включая главную диагональ.
            // Результат получить в виде массива размера 2n – 1, где n -размер стороны матрицы.
            // Заполнение массива элементами крайней (outermost/extreme) левой нижней диагонали с постепенным перемещением вправо вверх.

            // Sum the elements in each diagonal parallel to the main one, including the main diagonal itself
            // Return the result as an array of size "2n - 1", where n is the size of the matrix side
            // Fill the array with the elements of the leftmost bottom diagonal, moving gradually up and to the right

            //---------------------------------------------------------------------------------------------------------------------------------------------

            if (matrix.GetLength(0) != matrix.GetLength(1))
                return answer;

            int size = matrix.GetLength(0); // dimension of the square matrix
            answer = new int[2 * size - 1];

            int count = 1;
            for (int step = 0; step < answer.Length; step++)
            {
                if (step < size - 1)
                {
                    int length = size - 1, row = step;
                    for (int i = 0; i < step + 1; i++)
                    {
                        answer[step] += matrix[length, row];
                        length--;
                        row--;
                    }

                    continue;
                }

                if (step == size - 1)
                {
                    int length = 0, row = 0;
                    for (int i = 0; i < size; i++)
                    {
                        answer[step] += matrix[length, row];
                        length++;
                        row++;
                    }

                    continue;
                }

                if (step > size - 1)
                {
                    int length = size - 1 - count, row = size - 1;
                    for (int i = 0; i < size - count; i++)
                    {
                        answer[step] += matrix[length, row];
                        length--;
                        row--;
                    }

                    count++;
                }
            }

            return answer;



            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

            return answer;
        }
        public void Task9(int[,] matrix, int k)
        {

            // code here
            // PROBLEM NINE----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается квадратная матрица (двумерный массив) matrix и индекс строки (и столбца) k.
            // Найти максимальный по модулю элемент матрицы.
            // Сдвинуть строки и столбцы так, чтобы строка и столбец, на пересечении которых находится найденный элемент,
            // оказались на пересечении k-й строки и k - го столбца.
            // При этом взаимный порядок остальных строк и столбцов должен сохраняться.

            // Find the element of the matrix with the largest absolute value.
            // Shift the rows and columns so that the row and column containing this element are moved to the k-th row and the k-th column.
            // The relative order of the remaining rows and columns must be preserved.

            //---------------------------------------------------------------------------------------------------------------------------------------------

            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            if (n == 0 || m == 0 || n != m)
            {
                return;
            }

            if (k < 0 || k >= n)
            {
                return;
            }

            int maxRow = 0;
            int maxCol = 0;
            int maxAbs = Math.Abs(matrix[0, 0]);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int valueAbs = Math.Abs(matrix[i, j]);
                    if (valueAbs > maxAbs)
                    {
                        maxAbs = valueAbs;
                        maxRow = i;
                        maxCol = j;
                    }
                }
            }

            if (maxRow != k)
            {
                int[] tempRow = new int[n];

                for (int j = 0; j < n; j++)
                {
                    tempRow[j] = matrix[maxRow, j];
                }

                if (maxRow < k)
                {
                    for (int i = maxRow; i < k; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            matrix[i, j] = matrix[i + 1, j];
                        }
                    }
                }
                else
                {
                    for (int i = maxRow; i > k; i--)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            matrix[i, j] = matrix[i - 1, j];
                        }
                    }
                }

                for (int j = 0; j < n; j++)
                {
                    matrix[k, j] = tempRow[j];
                }

                maxRow = k;
            }

            if (maxCol != k)
            {
                int[] tempCol = new int[n];

                for (int i = 0; i < n; i++)
                {
                    tempCol[i] = matrix[i, maxCol];
                }

                if (maxCol < k)
                {
                    for (int j = maxCol; j < k; j++)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            matrix[i, j] = matrix[i, j + 1];
                        }
                    }
                }
                else
                {
                    for (int j = maxCol; j > k; j--)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            matrix[i, j] = matrix[i, j - 1];
                        }
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    matrix[i, k] = tempCol[i];
                }
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

        }
        public int[,] Task10(int[,] A, int[,] B)
        {
            int[,] answer = null;

            // code here
            // PROBLEM TEN----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается две матрицы (двумерные массивы) A и B.
            // Перемножить матрицы и получить результат в новой матрице (двумерном массиве).

            // Multiply the matrices and store the result in a new matrix

            //---------------------------------------------------------------------------------------------------------------------------------------------

            int rowsA = A.GetLength(0);
            int colsA = A.GetLength(1);

            int rowsB = B.GetLength(0);
            int colsB = B.GetLength(1);

            if (colsA != rowsB)
            {
                return null;
            }

            answer = new int[rowsA, colsB];

            int totalCells = rowsA * colsB;

            for (int index = 0; index < totalCells; index++)
            {
                int i = index / colsB;
                int j = index % colsB;

                int sum = 0;

                for (int k = 0; k < colsA; k++)
                {
                    sum += A[i, k] * B[k, j];
                }

                answer[i, j] = sum;
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

            return answer;
        }
        public int[][] Task11(int[,] matrix)
        {
            int[][] answer = null;

            // code here
            // PROBLEM ELEVEN----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается матрица (двумерный массив) matrix.
            // Удалить в каждой строке неположительные элементы.
            // Получить результат в виде зубчатого массива.
            // Не изменять порядок оставшихся положительных элементов.
            // Если после удаления строка окажется пуста,включить пустую строку в выходной массив.

            // Remove all non-positive elements from each row.
            // Return the result as a jagged array (i.e., with rows of different lengths)
            // Do not change the order of the remaining positive elements.
            // If a row becomes empty after removal, include an empty row in the output array

            //---------------------------------------------------------------------------------------------------------------------------------------------


            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            answer = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                int count = 0;

                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        count++;
                    }
                }

                int[] row = new int[count];
                int index = 0;

                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        row[index] = matrix[i, j];
                        index++;
                    }
                }

                answer[i] = row;
            }


            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

            return answer;
        }
        public int[,] Task12(int[][] array)
        {
            int[,] answer = null;

            // code here
            // PROBLEM TWELVE----------------------------------------------------------------------------------------------------------------------------------------------------------------------------START

            // В метод передается зубчатый массив (массив массивов) array.
            // Из его элементов создать квадратную матрицу минимального размера, заполнив недостающие элементы нулями.
            // Заполнить элементы по строкам слева направо, сверху вниз, беря элементы из array в нисходящем порядке:
            // все элементы первой строки слева направо, потом второй строки и т.д.

            // Create a square matrix of the minimal size from its elements, filling any missing elements with zeros.
            // Fill the matrix row by row, left to right, and top to bottom, taking elements from array in descending order:
            // All elements of the first row from left to right, then the second row, and so on, so forth.


            //---------------------------------------------------------------------------------------------------------------------------------------------

            int elementTotal = 0;

            for (int i = 0; i < array.Length; i++)
            {
                elementTotal += array[i].Length;
            }

            // Dimension of the square matrix
            int n = (int)Math.Ceiling(Math.Sqrt(elementTotal));

            answer = new int[n, n];

            int index = 0;
            int row = 0;
            int col = 0;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (index < Math.Pow(n, 2) )
                    {
                        int row = index / n;
                        int col = index % n;

                        answer[row, col] = array[i][j];
                        index++;
                    }
                }
            }

            //---------------------------------------------------------------------------------------------------------------------------------------------
            // end

            return answer;
        }
    }
}





