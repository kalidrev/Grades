using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public abstract class GradeTracker : IGradeTracker
    {
        public abstract void AddGrade(float grade);
        public abstract GradeStatistics ComputeStatistics();
        public abstract void WriteGrades(TextWriter destination);
        public abstract IEnumerator GetEnumerator();

        public NameChangedDelegate NameChanged;

        protected string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                NullNameCheck(value);

                if (IsNewName(value) && NameChangedHasSubscribers())
                {
                    NameChangedEventArgs args = new NameChangedEventArgs();
                    args.ExistingName = _name;
                    args.NewName = value;

                    NameChanged(this, args);
                }
                _name = value;
            }

        }

        private bool NameChangedHasSubscribers()
        {
            return NameChanged != null;
        }

        private bool IsNewName(string value)
        {
            return _name != value;
        }

        private static void NullNameCheck(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }
        }
    }
}
