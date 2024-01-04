using C971.Data;
using C971.Views;

namespace C971
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Admin), typeof(Admin));
            Routing.RegisterRoute(nameof(AdminReport), typeof(AdminReport));
            Routing.RegisterRoute(nameof(TermList), typeof(TermList));
            Routing.RegisterRoute(nameof(TermDetailed), typeof(TermDetailed));
            Routing.RegisterRoute(nameof(TermNew), typeof(TermNew));
            Routing.RegisterRoute(nameof(CourseList), typeof(CourseList));
            Routing.RegisterRoute(nameof(CourseDetailed), typeof(CourseDetailed));
            Routing.RegisterRoute(nameof(CourseDetailedStudent), typeof(CourseDetailedStudent));
            Routing.RegisterRoute(nameof(CourseNew), typeof(CourseNew));
            Routing.RegisterRoute(nameof(AssessmentList), typeof(AssessmentList));
            Routing.RegisterRoute(nameof(AssessmentDetailed), typeof(AssessmentDetailed));
            Routing.RegisterRoute(nameof(AssessmentDetailedStudent), typeof(AssessmentDetailedStudent));
            Routing.RegisterRoute(nameof(AssessmentNew), typeof(AssessmentNew));
            Routing.RegisterRoute(nameof(NoteList), typeof(NoteList));
            Routing.RegisterRoute(nameof(Note), typeof(Note));
            Routing.RegisterRoute(nameof(NoteNew), typeof(NoteNew));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(User), typeof(User));
            Routing.RegisterRoute(nameof(UserNew), typeof(UserNew));
            Routing.RegisterRoute(nameof(UserNoEdits), typeof(UserNoEdits));
            Routing.RegisterRoute(nameof(UserAddCourse), typeof(UserAddCourse));
        }
    }
}