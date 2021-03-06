﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebNotifier
{


    /// <summary>

    /// Gets the values from the AssemblyInfo.cs file for the current executing assembly

    /// </summary>

    /// <example>        

    /// string company = AssemblyInfo.Company;

    /// string product = AssemblyInfo.Product;

    /// string copyright = AssemblyInfo.Copyright;

    /// string trademark = AssemblyInfo.Trademark;

    /// string title = AssemblyInfo.Title;

    /// string description = AssemblyInfo.Description;

    /// string configuration = AssemblyInfo.Configuration;

    /// string file version = AssemblyInfo.FileVersion;

    /// Version version = AssemblyInfo.Version;

    /// string versionFull = AssemblyInfo.VersionFull;

    /// string versionMajor = AssemblyInfo.VersionMajor;

    /// string versionMinor = AssemblyInfo.VersionMinor;

    /// string versionBuild = AssemblyInfo.VersionBuild;

    /// string versionRevision = AssemblyInfo.VersionRevision;

    /// </example>

     public class AssemblyInfo

    {

        public  string Company => GetExecutingAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company);
        public  string Product => GetExecutingAssemblyAttribute<AssemblyProductAttribute>(a => a.Product);
        public  string Copyright => GetExecutingAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright);
        public  string Trademark => GetExecutingAssemblyAttribute<AssemblyTrademarkAttribute>(a => a.Trademark);
        public  string Title => GetExecutingAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title);
        public  string Description => GetExecutingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description);
        public  string Configuration => GetExecutingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description);
        public  string FileVersion => GetExecutingAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version);


        public  Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public  string VersionFull => Version.ToString();
        public  string VersionMajor => Version.Major.ToString();
        public  string VersionMinor => Version.Minor.ToString();
        public  string VersionBuild => Version.Build.ToString();
        public  string VersionRevision => Version.Revision.ToString();


        private  string GetExecutingAssemblyAttribute<T>(Func<T, string> value) where T : Attribute

        {

            T attribute = (T)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T));

            return value.Invoke(attribute);

        }

    }





    /// <summary>

    /// Gets the values from the AssemblyInfo.cs file for the previous assembly

    /// </summary>

    /// <example>

    /// AssemblyInfoCalling assembly = new AssemblyInfoCalling();

    /// string company1 = assembly.Company;

    /// string product1 = assembly.Product;

    /// string copyright1 = assembly.Copyright;

    /// string trademark1 = assembly.Trademark;

    /// string title1 = assembly.Title;

    /// string description1 = assembly.Description;

    /// string configuration1 = assembly.Configuration;

    /// string fileversion1 = assembly.FileVersion;

    /// Version version1 = assembly.Version;

    /// string versionFull1 = assembly.VersionFull;

    /// string versionMajor1 = assembly.VersionMajor;

    /// string versionMinor1 = assembly.VersionMinor;

    /// string versionBuild1 = assembly.VersionBuild;

    /// string versionRevision1 = assembly.VersionRevision;

    /// </example>

    public class AssemblyInfoCalling

    {

        /// <summary>

        /// Initializes a new instance of the <see cref="AssemblyInfoCalling"/> class.

        /// </summary>

        /// <param name="traceLevel">The trace level needed to get correct assembly 

        /// - will need to adjust based on where you put these classes in your project(s).</param>

        public AssemblyInfoCalling(int traceLevel = 4)

        {

            //----------------------------------------------------------------------

            // Default to "3" as the number of levels back in the stack trace to get the 

            //  correct assembly for "calling" assembly

            //----------------------------------------------------------------------

            StackTraceLevel = traceLevel;

        }



        //----------------------------------------------------------------------

        // Standard assembly attributes

        //----------------------------------------------------------------------

        public string Company => GetCallingAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company);
        public string Product => GetCallingAssemblyAttribute<AssemblyProductAttribute>(a => a.Product);
        public string Copyright => GetCallingAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright);
        public string Trademark => GetCallingAssemblyAttribute<AssemblyTrademarkAttribute>(a => a.Trademark);
        public string Title => GetCallingAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title);
        public string Description => GetCallingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description);
        public string Configuration => GetCallingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description);
        public string FileVersion => GetCallingAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version);


        //----------------------------------------------------------------------

        // Version attributes

        //----------------------------------------------------------------------

        public  Version Version

        {

            get

            {

                //----------------------------------------------------------------------

                // Get the assembly, return empty if null

                //----------------------------------------------------------------------

                Assembly assembly = GetAssembly(StackTraceLevel);

                return assembly == null ? new Version() : assembly.GetName().Version;

            }

        }

        public string VersionFull => Version.ToString();
        public string VersionMajor => Version.Major.ToString();
        public string VersionMinor => Version.Minor.ToString();
        public string VersionBuild => Version.Build.ToString();
        public string VersionRevision => Version.Revision.ToString();


        //----------------------------------------------------------------------

        // Set how deep in the stack trace we're looking - allows for customized changes

        //----------------------------------------------------------------------

        public  int StackTraceLevel { get; set; }



        //----------------------------------------------------------------------

        // Custom Attributes

        //----------------------------------------------------------------------

        //public string Location

        //{ 

        //    get

        //    {

        //        try

        //        {

        //            return GetCallingAssemblyAttribute<AssemblyLocationAttribute>(a => a.Value);

        //        }

        //        catch (NullReferenceException)

        //        {

        //            return string.Empty;

        //        }



        //    } 

        //}



        /// <summary>

        /// Gets the calling assembly attribute.

        /// </summary>

        /// <typeparam name="T"></typeparam>

        /// <param name="value">The value.</param>

        /// <example>return GetCallingAssemblyAttribute&lt;AssemblyCompanyAttribute&gt;(a => a.Company);</example>

        /// <returns></returns>

        private string GetCallingAssemblyAttribute<T>(Func<T, string> value) where T : Attribute

        {

            //----------------------------------------------------------------------

            // Get the assembly, return empty if null

            //----------------------------------------------------------------------

            Assembly assembly = GetAssembly(StackTraceLevel);

            if (assembly == null)
            {
                return string.Empty;
            }



            //----------------------------------------------------------------------

            // Get the attribute value

            //----------------------------------------------------------------------

            T attribute = (T)Attribute.GetCustomAttribute(assembly, typeof(T));

            return value.Invoke(attribute);

        }



        /// <summary>

        /// Go through the stack and gets the assembly

        /// </summary>

        /// <param name="stackTraceLevel">The stack trace level.</param>

        /// <returns></returns>

        private  Assembly GetAssembly(int stackTraceLevel)

        {

            //----------------------------------------------------------------------

            // Get the stack frame, returning null if none

            //----------------------------------------------------------------------

            StackTrace stackTrace = new StackTrace();

            StackFrame[] stackFrames = stackTrace.GetFrames();

            if (stackFrames == null)
            {
                return null;
            }



            //----------------------------------------------------------------------

            // Get the declaring type from the associated stack frame, returning null if nonw

            //----------------------------------------------------------------------

            Type declaringType = stackFrames[stackTraceLevel].GetMethod().DeclaringType;

            if (declaringType == null)
            {
                return null;
            }



            //----------------------------------------------------------------------

            // Return the assembly

            //----------------------------------------------------------------------

            Assembly assembly = declaringType.Assembly;

            return assembly;

        }

    }


}
