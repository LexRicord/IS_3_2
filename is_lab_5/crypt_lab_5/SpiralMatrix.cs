using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;

namespace crypt_lab_5_matrix
{
    class SpiralMatrix
    {
        internal static int[,] initMatrix(int m, int n, int start, int step)
        {
            int[,] ret = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ret[i, j] = start;
                    start += step;
                }
            }
            return ret;
        }
        internal static char[,] initMatrix(int m, int n, char[] input)
        {
            char[,] ret = new char[m, n];
            int su1 = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {    
                    ret[i, j] = input[su1];
                    su1++;
                }
            }
            return ret;
        }
        internal static char[,] initMatrix(int m, int n, char[,] mat)
        {
            char[,] ret = new char[m, n];
            ret = mat;
            return ret;
        }
        internal static void displayMatrix(char[,] mat, int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("\t{0}", mat[i, j]);
                }
                Console.WriteLine();
            }
        }
        internal static List<char> spiralOrder(char[,] matrix)
        {
            List<char> ans = new List<char>();

            if (matrix.Length == 0)
                return ans;
            int R = matrix.GetLength(0), C = matrix.GetLength(1);
            bool[,] seen = new bool[R, C];
            int[] dr = { 0, 1, 0, -1 };
            int[] dc = { 1, 0, -1, 0 };
            int r = 0, c = 0, di = 0;

            for (int i = 0; i < R * C; i++)
            {
                ans.Add(matrix[r, c]);
                seen[r, c] = true;
                int cr = r + dr[di];
                int cc = c + dc[di];

                if (0 <= cr && cr < R && 0 <= cc && cc < C
                    && !seen[cr, cc])
                {
                    r = cr;
                    c = cc;
                }
                else
                {
                    di = (di + 1) % 4;
                    r += dr[di];
                    c += dc[di];
                }
            }
            return ans;
        }

        internal static char[,] ReverseSpiralInput(int m, int n, char[] input)
        {
            char[,] matrix = new char[m, n];
            int i, k = 0, l = 0;

            int z = 0;

            int size = m * n;

            while (k < m && l < n)
            {
                int val;

                for (i = l; i < n; ++i)
                {

                    matrix[k,i] = input[z];
                    ++z;
                }
                k++;

                for (i = k; i < m; ++i)
                {

                    matrix[i, n - 1] = input[z];
                    ++z;
                }
                n--;

                if (k < m)
                {
                    for (i = n - 1; i >= l; --i)
                    {
                        matrix[m - 1, i] = input[z];
                        ++z;
                    }
                    m--;
                }
                if (l < n)
                {
                    for (i = m - 1; i >= k; --i)
                    {
                        matrix[i, l] = input[z];
                        ++z;
                    }
                    l++;
                }
            }
            return matrix;
        }

        internal static char[] ReverseSpiralPrint(int m, int n, char[,] a)
        {

            char[] b = new char[n * m];

            int i, k = 0, l = 0;

            int z = 0;

            int size = m * n;

            while (k < m && l < n)
            {
                char val;
                for (i = l; i < n; ++i)
                {
                    val = a[k, i];
                    b[z] = val;
                    ++z;
                }
                k++;

                for (i = k; i < m; ++i)
                {
                    val = a[i, n - 1];
                    b[z] = val;
                    ++z;
                }
                n--;

                if (k < m)
                {
                    for (i = n - 1; i >= l; --i)
                    {
                        val = a[m - 1, i];
                        b[z] = val;
                        ++z;
                    }
                    m--;
                }

                if (l < n)
                {
                    for (i = m - 1; i >= k; --i)
                    {
                        // printf("%d ", a[i][l]);
                        val = a[i, l];
                        b[z] = val;
                        ++z;
                    }
                    l++;
                }
            }
            return b;
        }
    }
}