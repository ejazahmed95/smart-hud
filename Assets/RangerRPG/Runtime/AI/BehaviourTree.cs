using System.Collections.Generic;
using UnityEngine;

namespace RangerRPG.AI {
    public class BehaviourTree {
        public BTNode root;
        
        public BehaviourTree() {
            root = new BTSelector("Root");
        }
    
        public BehaviourTree(string treeName) {
            root = new BTNode(treeName);
        }

        public BTNode.Status Run() {
            return root.Process();
        }
    
        // Printing the tree
        struct NodeLevel {
            public int level;
            public BTNode node;
        }
        
        public void PrintTree() {
            string treePrintOut = "";
            Stack<NodeLevel> nodeStack = new Stack<NodeLevel>();
            BTNode currentNode = root;
            nodeStack.Push(new NodeLevel { level = 0, node = currentNode });
    
            while (nodeStack.Count != 0) {
    
                NodeLevel nextNode = nodeStack.Pop();
                treePrintOut += new string('-', nextNode.level*2) + nextNode.node.name + "\n";
    
                for (int i = nextNode.node.children.Count - 1; i >= 0; --i) {
    
                    nodeStack.Push(new NodeLevel { level = nextNode.level + 1, node = nextNode.node.children[i] });
                }
            }
            Debug.Log(treePrintOut);
        }
    }
}