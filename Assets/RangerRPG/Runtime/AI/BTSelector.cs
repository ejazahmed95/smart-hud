namespace RangerRPG.AI {
    public class BTSelector: BTNode {

        public BTSelector(string n) : base(n) {}
        
        public override Status Process() {

            var childStatus = children[currentChild].Process();

            switch (childStatus) {
                case Status.RUNNING:
                    return Status.RUNNING;
                case Status.SUCCESS:
                    currentChild = 0;
                    return Status.SUCCESS;
                case Status.FAILURE:
                    if (++currentChild < children.Count) {
                        return Status.RUNNING;
                    }
                    currentChild = 0;
                    return Status.FAILURE;
                default:
                    return Status.FAILURE;
            }
            
        }
    }
}