using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Start without debugging is required to run the program
//The program will check if the tree is an AVL tree or not
//The program will also print the in-order traversal of the tree

namespace BinarySearchTrees
{
    public class TreeNode
    {
        public int Value;
        public TreeNode Left;
        public TreeNode Right;
        public int Height;

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
            Height = 1;
        }
    }

    public class AVLTree
    {
        private TreeNode root;

        private int Height(TreeNode node)
        {
            if (node == null)
                return 0;
            return node.Height;
        }

        private int BalanceFactor(TreeNode node)
        {
            if (node == null)
                return 0;
            return Height(node.Left) - Height(node.Right);
        }

        private TreeNode RotateRight(TreeNode y)
        {
            TreeNode x = y.Left;
            TreeNode T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private TreeNode RotateLeft(TreeNode x)
        {
            TreeNode y = x.Right;
            TreeNode T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        public TreeNode Insert(TreeNode node, int value)
        {
            if (node == null)
                return new TreeNode(value);

            if (value < node.Value)
                node.Left = Insert(node.Left, value);
            else if (value > node.Value)
                node.Right = Insert(node.Right, value);
            else
                return node; // Duplicate values not allowed

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = BalanceFactor(node);

            // Left Left Case
            if (balance > 1 && value < node.Left.Value)
                return RotateRight(node);

            // Right Right Case
            if (balance < -1 && value > node.Right.Value)
                return RotateLeft(node);

            // Left Right Case
            if (balance > 1 && value > node.Left.Value)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            // Right Left Case
            if (balance < -1 && value < node.Right.Value)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        public void Insert(int value)
        {
            root = Insert(root, value);
        }

        public bool IsAVL(TreeNode node)
        {
            if (node == null)
                return true;

            int balance = BalanceFactor(node);
            if (Math.Abs(balance) > 1)
                return false;

            return IsAVL(node.Left) && IsAVL(node.Right);
        }

        public bool IsAVL()
        {
            return IsAVL(root);
        }

        public void InOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.Write(node.Value + " ");
                InOrderTraversal(node.Right);
            }
        }

        public void InOrderTraversal()
        {
            InOrderTraversal(root);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] elements = { 45, 27, 67, 36, 56, 15, 75, 31, 53, 39, 64 };
            AVLTree tree = new AVLTree();

            foreach (int element in elements)
            {
                tree.Insert(element);
            }

            Console.WriteLine("In-order traversal of the tree:");
            tree.InOrderTraversal();
            Console.WriteLine();

            if (tree.IsAVL())
                Console.WriteLine("The tree is an AVL tree.");
            else
                Console.WriteLine("The tree is not an AVL tree.");
            Console.WriteLine();
        }
    }
}
