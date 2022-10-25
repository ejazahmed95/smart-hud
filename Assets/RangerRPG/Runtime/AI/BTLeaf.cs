namespace RangerRPG.AI {
    public class BTLeaf: BTNode {
        public delegate BTNode.Status Tick();
        public Tick ProcessMethod;
        
        public BTLeaf(string n, Tick pm) : base(n) {
            ProcessMethod = pm;
        }

        public override Status Process() {
            return ProcessMethod();
        }
    }
}