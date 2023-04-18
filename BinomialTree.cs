namespace BinomialPyramid
{
    public class BinomialTreeNode<T>
    {
        public T Value { get; set; }
        public int Degree { get; set; }
        public BinomialTreeNode<T> Parent { get; set; }
        public BinomialTreeNode<T> Child { get; set; }
        public BinomialTreeNode<T> Sibling { get; set; }

        public BinomialTreeNode(T value)
        {
            Value = value;
            Degree = 0;
            Parent = null;
            Child = null;
            Sibling = null;
        }
    }

    public class BinomialTree<T> where T : IComparable<T>
    {
        private BinomialTreeNode<T> root;

        public BinomialTree()
        {
            root = null;
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        public void Insert(T value)
        {
            BinomialTree<T> tree = new BinomialTree<T>();
            tree.root = new BinomialTreeNode<T>(value);
            root = MergeTrees(this.root, tree.root);
        }

        public void Remove(T value)
        {
            if (root == null)
            {
                return;
            }

            BinomialTreeNode<T> node = Search(value).Item2;
            if (node == null)
            {
                return;
            }

            RemoveNode(node);
        }

        public (bool, BinomialTreeNode<T>?) Search(T value)
        {
            var node = SearchNode(root, value);

            if (node is null) 
                return (false, null);

            return (true, node);
        }

        private BinomialTreeNode<T> SearchNode(BinomialTreeNode<T> node, T value)
        {
            if (node == null)
            {
                return null;
            }

            if (node.Value.CompareTo(value) == 0)
            {
                return node;
            }

            BinomialTreeNode<T> foundNode = SearchNode(node.Child, value);
            if (foundNode == null)
            {
                foundNode = SearchNode(node.Sibling, value);
            }

            return foundNode;
        }

        private void RemoveNode(BinomialTreeNode<T> node)
        {
            DecreaseKey(node, default(T));
            RemoveMin();
        }

        private void DecreaseKey(BinomialTreeNode<T> node, T newValue)
        {
            node.Value = newValue;
            BinomialTreeNode<T> parent = node.Parent;
            while (parent != null && node.Value.CompareTo(parent.Value) < 0)
            {
                T tempValue = node.Value;
                node.Value = parent.Value;
                parent.Value = tempValue;

                node = parent;
                parent = node.Parent;
            }
        }

        private void RemoveMin()
        {
            if (root == null)
            {
                return;
            }

            BinomialTreeNode<T> minNode = root;
            BinomialTreeNode<T> prevNode = null;
            BinomialTreeNode<T> currentNode = root.Sibling;
            while (currentNode != null)
            {
                if (currentNode.Value.CompareTo(minNode.Value) < 0)
                {
                    minNode = currentNode;
                    prevNode = root;
                }
                else
                {
                    prevNode = currentNode;
                }

                currentNode = currentNode.Sibling;
            }

            if (prevNode == null && minNode == root)
            {
                root = root.Child;
            }
            else if (prevNode == null && minNode != root)
            {
                root = minNode;
            }
            else if (prevNode.Sibling == minNode)
            {
                prevNode.Sibling = minNode.Sibling;
            }
            else
            {
                prevNode.Child = minNode.Sibling;
            }
            BinomialTree<T> newTree = new BinomialTree<T>();
            BinomialTreeNode<T> childNode = minNode.Child;
            if (childNode != null)
            {
                newTree.root = childNode;
                BinomialTreeNode<T> currentChildNode = childNode.Sibling;
                childNode.Sibling = null;
                BinomialTreeNode<T> prevChildNode = childNode;
                while (currentChildNode != null)
                {
                    prevChildNode.Sibling = currentChildNode.Sibling;
                    currentChildNode.Sibling = newTree.root;
                    newTree.root = currentChildNode;

                    currentChildNode = prevChildNode.Sibling;
                    prevChildNode = prevChildNode.Sibling;
                }
            }

            root = MergeTrees(this.root, newTree.root);
        }
        private BinomialTreeNode<T> MergeTrees(BinomialTreeNode<T> tree1, BinomialTreeNode<T> tree2)
        {
            if (tree1 == null) return tree2;
            if (tree2 == null) return tree1;

            BinomialTreeNode<T> result;
            if (tree1.Value.CompareTo(tree2.Value) < 0)
            {
                result = tree1;
                result.Sibling = MergeTrees(tree1.Sibling, tree2);
            }
            else
            {
                result = tree2;
                result.Sibling = MergeTrees(tree1, tree2.Sibling);
            }

            return result;
        }

        private BinomialTreeNode<T> LinkTrees(BinomialTreeNode<T> tree1, BinomialTreeNode<T> tree2)
        {
            if (tree1.Value.CompareTo(tree2.Value) > 0)
            {
                BinomialTreeNode<T> temp = tree1;
                tree1 = tree2;
                tree2 = temp;
            }

            tree2.Parent = tree1;
            tree2.Sibling = tree1.Child;
            tree1.Child = tree2;
            tree1.Degree++;

            return tree1;
        }
    }
}
