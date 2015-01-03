﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShortcutRunner.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ShortcutRunner.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ShortcutRunner is already running. Plase close other instance before running this one..
        /// </summary>
        internal static string ApplicationAlreadyRunningException {
            get {
                return ResourceManager.GetString("ApplicationAlreadyRunningException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You can close ShortcutRunner using try icon..
        /// </summary>
        internal static string BalloonTipText {
            get {
                return ResourceManager.GetString("BalloonTipText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shortcut Runner.
        /// </summary>
        internal static string BalloonTipTitle {
            get {
                return ResourceManager.GetString("BalloonTipTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ShortcutRunner error.
        /// </summary>
        internal static string ErrorMessageCaption {
            get {
                return ResourceManager.GetString("ErrorMessageCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There was an error while registering &apos;{0}&apos; shortcut. It is probably already registered by other application..
        /// </summary>
        internal static string HotkeyRegistrationException {
            get {
                return ResourceManager.GetString("HotkeyRegistrationException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon IconBlack {
            get {
                object obj = ResourceManager.GetObject("IconBlack", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon IconWhite {
            get {
                object obj = ResourceManager.GetObject("IconWhite", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Line {0} does not match shortcut description format:.
        /// </summary>
        internal static string InvalidConfigurationLineException {
            get {
                return ResourceManager.GetString("InvalidConfigurationLineException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shortcut at line {0} is invalid:.
        /// </summary>
        internal static string InvalidShortcutInConfigurationException {
            get {
                return ResourceManager.GetString("InvalidShortcutInConfigurationException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Key &apos;{0}&apos; was not recognized in &apos;{1}&apos; shortcut..
        /// </summary>
        internal static string KeyNotRecognizedException {
            get {
                return ResourceManager.GetString("KeyNotRecognizedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shortcut &apos;{0}&apos; conatins more than one non-modifier key..
        /// </summary>
        internal static string MultipleNonModifierKeysException {
            get {
                return ResourceManager.GetString("MultipleNonModifierKeysException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shortcut &apos;{0}&apos; contains no non-modifier key..
        /// </summary>
        internal static string NoNonModifierKeysException {
            get {
                return ResourceManager.GetString("NoNonModifierKeysException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exit ShortcutRunner.
        /// </summary>
        internal static string TryIconExitMenuItem {
            get {
                return ResourceManager.GetString("TryIconExitMenuItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shortcut Runner.
        /// </summary>
        internal static string TryIconTitle {
            get {
                return ResourceManager.GetString("TryIconTitle", resourceCulture);
            }
        }
    }
}
