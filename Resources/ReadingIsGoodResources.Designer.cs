﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReadingIsGood.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ReadingIsGoodResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ReadingIsGoodResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ReadingIsGood.Resources.ReadingIsGoodResources", typeof(ReadingIsGoodResources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to sessionList.
        /// </summary>
        public static string CacheSessionListKey {
            get {
                return ResourceManager.GetString("CacheSessionListKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} alanı boş olamaz..
        /// </summary>
        public static string Error_EmptyParameter {
            get {
                return ResourceManager.GetString("Error_EmptyParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bu işlemi yapmak için yetkili değilsiniz..
        /// </summary>
        public static string Error_NotAuthorizedUser {
            get {
                return ResourceManager.GetString("Error_NotAuthorizedUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stokta istediğiniz kadar kitap bulunmamaktadır..
        /// </summary>
        public static string Error_NotEnoughBookAtStock {
            get {
                return ResourceManager.GetString("Error_NotEnoughBookAtStock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to İstediğiniz kitap stoklarda bulunamadı..
        /// </summary>
        public static string Error_NotFoundBookAtStock {
            get {
                return ResourceManager.GetString("Error_NotFoundBookAtStock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} alanı sıfır veya sıfırdan küçük girilemez..
        /// </summary>
        public static string Error_ParameterCantBeLessThanOrEqualToZero {
            get {
                return ResourceManager.GetString("Error_ParameterCantBeLessThanOrEqualToZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Girdiğiniz şifreler birbirinden farklıdır..
        /// </summary>
        public static string Error_PasswordIsNotMatched {
            get {
                return ResourceManager.GetString("Error_PasswordIsNotMatched", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kullanıcıya ait sisteme gereçli giriş bulunamadı. Lütfen tekrar giriş yapın..
        /// </summary>
        public static string Error_SessionNotFound {
            get {
                return ResourceManager.GetString("Error_SessionNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kullanıcı bilgisi bulunamadı..
        /// </summary>
        public static string Error_UserInfosAreNotValid {
            get {
                return ResourceManager.GetString("Error_UserInfosAreNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} kullanıcısı sisteme giriş yaptı..
        /// </summary>
        public static string Info_AuthenticationSuccess {
            get {
                return ResourceManager.GetString("Info_AuthenticationSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} kullanıcısı için doğrulama başarılı..
        /// </summary>
        public static string Info_AuthorizationSuccess {
            get {
                return ResourceManager.GetString("Info_AuthorizationSuccess", resourceCulture);
            }
        }
    }
}
