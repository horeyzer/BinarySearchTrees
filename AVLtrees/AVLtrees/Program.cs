using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLtrees
{
    public class TreeNode
    {
        public int data;
        public TreeNode left;
        public TreeNode right;
        public int height;

        public TreeNode(int value)
        {
            data = value;
            left = null;
            right = null;
            height = 1;
        }
    }

    public class AVLTree
    {
        private TreeNode root;
        
        private int height(TreeNode node)
        {
            if (node == null)
                return 0;
            return node.height;
        }

        private int BalanceFactor(TreeNode node)
        {
            if (node == null)
                return 0;
            return height(node.left) - height(node.right);
        }

        private TreeNode RightRotate(TreeNode y)
        {
            TreeNode x = y.left;
            TreeNode T2 = x.right;

            x.right = y;
            y.left = T2;

            y.height = 1 + Math.Max(height(y.left), height(y.right));
            x.height = 1 + Math.Max(height(x.left), height(x.right));

            return x;
        }

        private TreeNode LeftRotate(TreeNode x)
        {
            TreeNode y = x.right;
            TreeNode T2 = y.left;

            y.left = x;
            x.right = T2;

            x.height = 1 + Math.Max(height(x.left), height(x.right));
            y.height = 1 + Math.Max(height(y.left), height(y.right));

            return y;
        }

        public TreeNode Insert(TreeNode node, int value)
        {
            if (node == null)
                return new TreeNode(value);

            if (value < node.data)
                node.left = Insert(node.left, value);
            else if (value > node.data)
                node.right = Insert(node.right, value);
            else
                return node; // Duplicate values are not allowed

            node.height = 1 + Math.Max(height(node.left), height(node.right));

            int balance = BalanceFactor(node);

            // Left Left Case
            if (balance > 1 && value < node.left.data)
                return RightRotate(node);

            // Right Right Case
            if (balance < -1 && value > node.right.data)
                return LeftRotate(node);

            // Left Right Case
            if (balance > 1 && value > node.left.data)
            {
                node.left = LeftRotate(node.left);
                return RightRotate(node);
            }

            // Right Left Case
            if (balance < -1 && value < node.right.data)
            {
                node.right = RightRotate(node.right);
                return LeftRotate(node);
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

            if (balance > 1 || balance < -1) // (Math.Abs(balance) > 1)
                return false;

            return IsAVL(node.left) && IsAVL(node.right);
        }

        public bool IsAVL()
        {
            return IsAVL(root);
        }

        public void InOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                InOrderTraversal(node.left);
                Console.Write(node.data + " ");
                InOrderTraversal(node.right);
            }
        }

        public void InOrderTraversal()
        {
            InOrderTraversal(root);
        }

        public void PostOrderTraversal(TreeNode node)
        {
            if (node != null)
            {
                PostOrderTraversal(node.left);
                PostOrderTraversal(node.right);
                Console.Write(node.data + " ");
            }
        }

        public void PostOrderTraversal()
        {
            PostOrderTraversal(root);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create an AVL tree
            AVLTree tree = new AVLTree();
            int[] elements = { 45, 27, 67, 36, 56, 15, 75, 31, 53, 39, 64 };
            foreach (int element in elements)
            {
                tree.Insert(element);
            }

            Console.WriteLine("Contents of the AVL tree (In-order traversal):");
            tree.InOrderTraversal();
            Console.WriteLine();

            // Add element(s) to make the tree unbalanced
            tree.Insert(20);

            Console.WriteLine("Contents of the unbalanced AVL tree (In-order traversal):");
            tree.InOrderTraversal();
            Console.WriteLine();

            // Rebalance the tree
            if (!tree.IsAVL())
            {
                Console.WriteLine("AVL tree is unbalanced. Performing rotations to rebalance...");
                tree.Insert(22); // One more element to make the tree unbalanced
            }

            Console.WriteLine("Content of the balanced AVL tree (In-order traversal):");
            tree.InOrderTraversal();
            Console.WriteLine();

            Console.WriteLine("Content of the AVL tree (Post-order traversal):");
            tree.PostOrderTraversal();
            Console.WriteLine();

            Console.WriteLine();
        }
    }
}
