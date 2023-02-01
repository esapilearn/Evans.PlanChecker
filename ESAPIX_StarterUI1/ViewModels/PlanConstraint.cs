using ESAPIX.Constraints;
using Prism.Mvvm;

namespace ESAPX_StarterUI.ViewModels
{
    public class PlanConstraint : BindableBase //Class to represent each row / "Constraint" to be evaluated and the result from the constraint
    {
        public PlanConstraint(IConstraint con)
        {
            this.Constraint = con;
        }

        private IConstraint _constraint; //Doing the more complex formal format here (vs. the set get) b/c programming against the user interface, which needs to know what has been changed
        public IConstraint Constraint
        {
            get { return _constraint; }
            set { SetProperty(ref _constraint, value); }
        }

        private ConstraintResult _result;
        public ConstraintResult Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }
    }
}