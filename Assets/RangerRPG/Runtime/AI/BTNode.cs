using System.Collections.Generic;

namespace RangerRPG.AI {
    public class BTNode {
        public enum Status {
            SUCCESS,
            RUNNING,
            FAILURE
        };

        public List<BTNode> children = new();
        public Status status;
        public int currentChild = 0;
        public string name;

        public BTNode(string n) {
            name = n;
        }

        public virtual Status Process() {
            if (currentChild >= children.Count) return Status.SUCCESS;
            return children[currentChild].Process();
        }

        public void AddChild(BTNode n) {
            children.Add(n);
        }

        public BTNode AddChildren(params BTNode[] nodes) {
            foreach (var node in nodes) {
                children.Add(node);
            }
            return this;
        }
    }
}