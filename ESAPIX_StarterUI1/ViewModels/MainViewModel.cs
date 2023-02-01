using ESAPIX.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESAPIX.Common;
using VMS.TPS.Common.Model.API;
using Prism.Commands;
using System.Windows;
using ESAPIX.Extensions;
using ESAPIX.Constraints.DVH;
using System.Collections.ObjectModel;

namespace ESAPX_StarterUI.ViewModels
{
    public class MainViewModel : BindableBase
    {
        AppComThread VMS = AppComThread.Instance;

        public MainViewModel()
        {
            EvaluateCommand = new DelegateCommand(() =>
            {
                foreach (var pc in Constraints)
                {
                    var result = VMS.GetValue(sc =>
                    {
                        //Check if we can constrain first
                        var canConstrain = pc.Constraint.CanConstrain(sc.PlanSetup); //Using a constraint framework here. Rex @ UAB added "CanConstrain" sub-method to see if is possible to constrain
                        //If not..report why
                        if (!canConstrain.IsSuccess) { return canConstrain; }
                        else
                        {
                            //Can constrain - so do it
                            return pc.Constraint.Constrain(sc.PlanSetup);
                        }
                    });
                    //Update UI
                    pc.Result = result;
                }
            });


            CreateConstraints();
        }

        private void CreateConstraints()
        {
            Constraints.AddRange(new PlanConstraint[] //Red squiggly remedied with lightbulb tool to create new class file "PlanConstraint.cs"
            {
                new PlanConstraint(ConstraintBuilder.Build("PTV45", "Max[%] <= 110")), //"ConstraintBuilder" is a helper method built by Rex to compare DVH to a constraint.  Can access free-ware on github.com/RexCardan
                new PlanConstraint(ConstraintBuilder.Build("Rectum", "V75Gy[%] <= 15")), //Can use this as a template to hardcode constraints for local clinical use.
                new PlanConstraint(ConstraintBuilder.Build("Rectum", "V65Gy[%] <= 35")),
                new PlanConstraint(ConstraintBuilder.Build("Bladder", "V80Gy[%] <= 15")),
                new PlanConstraint(new CTDateConstraint()) //This is the CT-Date_Constraint feature trying to branch
            });
        }


        public DelegateCommand EvaluateCommand { get; set; }
        public ObservableCollection<PlanConstraint> Constraints { get; private set; } = new ObservableCollection<PlanConstraint>();
    }
}