﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DLUTToolBox_V2.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DLUTToolBox_V2.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
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
        ///   查找类似 $(&quot;#factorycode&quot;).val(&apos;E001&apos;);
        ///    $(&quot;#xqh&quot;).val(&apos;03&apos;);
        ///    var url = &quot;/business/queryEleInfoByIdserial&quot;;
        ///    var param = new Object();
        ///    param.idserial = $(&quot;#idserial&quot;).val();
        ///    param.xqh = $(&quot;#xqh&quot;).val();
        ///    dkyw.request.post(url, param, function (data) {
        ///      if (data &amp;&amp; data.success) {
        ///        var resultData = data.resultData;
        ///        if (dkywcommon.isEmpty(resultData.jzwbh)) {} else if (dkywcommon.isEmpty(resultData.fjlc)) {} else if (dkywcommon.isEmpty(resultData.fjmc)) {
        ///
        ///        }  [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string Eleget {
            get {
                return ResourceManager.GetString("Eleget", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 $(&quot;#factorycode&quot;).val(&apos;E001&apos;);
        ///    $(&quot;#xqh&quot;).val(&apos;03&apos;);
        ///    var url = &quot;/business/queryEleInfoByIdserial&quot;;
        ///    var param = new Object();
        ///    param.idserial = $(&quot;#idserial&quot;).val();
        ///    param.xqh = $(&quot;#xqh&quot;).val();
        ///    dkyw.request.post(url, param, function (data) {
        ///      if (data &amp;&amp; data.success) {
        ///        var resultData = data.resultData;
        ///        if (dkywcommon.isEmpty(resultData.jzwbh)) {} else if (dkywcommon.isEmpty(resultData.fjlc)) {} else if (dkywcommon.isEmpty(resultData.fjmc)) {
        ///
        ///        }  [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string jsfunc {
            get {
                return ResourceManager.GetString("jsfunc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 function send(eleinfo) {
        ///    var url = &quot;/business/queryResEleByIdserial&quot;;
        ///    var param = new Object();
        ///    param.idserial = $(&quot;#idserial&quot;).val();
        ///    param.xqh = $(&quot;#xqh&quot;).val();
        ///    dkyw.request.post(url, param, function(data){
        ///        if(data&amp;&amp;data.success){
        ///            var data1 = data.resultData;
        ///            if (&quot;E001&quot; == $(&quot;#factorycode&quot;).val() || &quot;E002&quot; == $(&quot;#factorycode&quot;).val()) {
        ///                $(&quot;#roombalance&quot;).val(dkywcommon.fenToYuan(dkywcommon.yuanToFen(data1.sydl)));
        ///				window.chr [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string Send {
            get {
                return ResourceManager.GetString("Send", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;td height=&quot;20&quot; style=&quot;color:red;font-size:12px;&quot; colspan=&quot;2&quot;&gt; 的本地化字符串。
        /// </summary>
        internal static string Split {
            get {
                return ResourceManager.GetString("Split", resourceCulture);
            }
        }
    }
}
