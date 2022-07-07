using System.ComponentModel;
using System.Reflection;

namespace AnimesAPI.Enums {
    public enum StatusEnum {
        [Description("PlanToWatch")]
        PlanToWatch = 0,
        [Description("Completed")]
        Completed = 1,
        [Description("Watching")]
        Watching = 2,
        [Description("Dropped")]
        Dropped = 3,
    }
}
