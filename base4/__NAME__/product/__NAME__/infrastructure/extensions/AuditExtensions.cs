namespace __NAME__.infrastructure.extensions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using model.auditing;

    public static class AuditExtensions
    {
        public static string safe_get_value(this PropertyInfo pi, object target)
        {
            string return_value = string.Empty;

            if (target != null)
            {
                object v = pi.GetValue(target, null);
                if (v != null)
                {
                    return_value = v.ToString();
                }
            }

            return return_value;
        }

        public static object SafeIndex(this object[] array, int index)
        {
            if (array == null) return null;

            int arrayLength = array.Length;
            if (index <= arrayLength - 1)
            {
                return array[index];
            }

            return null;
        }

        public static bool is_audited(this Type t)
        {
            object[] attributes = t.GetCustomAttributes(true);

            foreach (var attribute in attributes)
            {
                if (attribute is DontChangeLogAuditThisAttribute)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool is_manual_change_audited(this Type t)
        {
            object[] attributes = t.GetCustomAttributes(true);

            foreach (var attribute in attributes)
            {
                if (attribute is LogManualChangesOnlyAttribute)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool is_restricted_data(this Type t)
        {
            object[] attributes = t.GetCustomAttributes(true);

            foreach (var attribute in attributes)
            {
                if (attribute is RestricedDataAttribute)
                {
                    return true;
                }
            }

            return false;
        }

        public static HashSet<string> get_audited_properties(this Type t)
        {
            HashSet<string> audited_properties = new HashSet<string>();

            foreach (var property_info in t.GetProperties())
            {
                if (property_is_audited(property_info))
                {
                    audited_properties.Add(t.FullName + "." + property_info.Name);
                }
            }

            return audited_properties;
        }

        public static HashSet<string> get_restricted_properties(this Type t)
        {
            HashSet<string> restricted_properties = new HashSet<string>();

            foreach (var property_info in t.GetProperties())
            {
                if (property_is_restricted(property_info))
                {
                    restricted_properties.Add(t.FullName + "." + property_info.Name);
                }
            }

            return restricted_properties;
        }

        public static bool property_is_audited(PropertyInfo property_info)
        {
            object[] attributes = property_info.GetCustomAttributes(true);

            foreach (var attribute in attributes)
            {
                if (attribute is DontChangeLogAuditThisAttribute)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool property_is_restricted(PropertyInfo property_info)
        {
            object[] attributes = property_info.GetCustomAttributes(true);

            foreach (var attribute in attributes)
            {
                if (attribute is RestricedDataAttribute)
                {
                    return true;
                }
            }

            return false;
        }
    }
}