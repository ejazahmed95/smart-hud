
namespace RangerRPG.AI {
    public class BTSequence : BTNode {

        public BTSequence(string n) : base(n) {
            
        }
        
        public override Status Process() {
            var childStatus = children[currentChild].Process();
            
            switch (childStatus) {
                case Status.RUNNING:
                    return Status.RUNNING;
                case Status.FAILURE:
                    return childStatus;
                case Status.SUCCESS:
                    if (++currentChild < children.Count) {
                        return Status.RUNNING;
                    }
                    currentChild = 0;
                    return Status.SUCCESS;
                default:
                    break;
            }
            return Status.FAILURE;
        }
    }
}