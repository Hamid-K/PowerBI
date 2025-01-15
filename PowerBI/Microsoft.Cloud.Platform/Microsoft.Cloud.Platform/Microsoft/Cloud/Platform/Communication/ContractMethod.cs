using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.CommunicationFramework.Attributes;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004E4 RID: 1252
	internal class ContractMethod
	{
		// Token: 0x060025EF RID: 9711 RVA: 0x00086CFC File Offset: 0x00084EFC
		internal ContractMethod(ContractObjectModel com, MethodInfo method)
		{
			this.Com = com;
			this.Method = method;
			this.BalancingKeys = new List<ECFParameter>();
			this.RemovableParameters = new List<ECFParameter>();
			this.HeaderHttpParameters = new List<ECFParameter>();
			this.HeaderSoapParameters = new List<ECFParameter>();
			this.ParseMethod();
			this.ExtendedResultAnalysis();
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x00086D55 File Offset: 0x00084F55
		// (set) Token: 0x060025F1 RID: 9713 RVA: 0x00086D5D File Offset: 0x00084F5D
		private ContractObjectModel Com { get; set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x00086D66 File Offset: 0x00084F66
		// (set) Token: 0x060025F3 RID: 9715 RVA: 0x00086D6E File Offset: 0x00084F6E
		public MethodInfo Method { get; private set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x00086D77 File Offset: 0x00084F77
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x00086D7F File Offset: 0x00084F7F
		public ICollection<ECFParameter> BalancingKeys { get; private set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x00086D88 File Offset: 0x00084F88
		// (set) Token: 0x060025F7 RID: 9719 RVA: 0x00086D90 File Offset: 0x00084F90
		public ICollection<ECFParameter> RemovableParameters { get; private set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x00086D99 File Offset: 0x00084F99
		// (set) Token: 0x060025F9 RID: 9721 RVA: 0x00086DA1 File Offset: 0x00084FA1
		public ICollection<ECFParameter> HeaderHttpParameters { get; private set; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x00086DAA File Offset: 0x00084FAA
		// (set) Token: 0x060025FB RID: 9723 RVA: 0x00086DB2 File Offset: 0x00084FB2
		public ICollection<ECFParameter> HeaderSoapParameters { get; private set; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x00086DBB File Offset: 0x00084FBB
		// (set) Token: 0x060025FD RID: 9725 RVA: 0x00086DC3 File Offset: 0x00084FC3
		public ContractMethodType MethodType { get; internal set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x00086DCC File Offset: 0x00084FCC
		// (set) Token: 0x060025FF RID: 9727 RVA: 0x00086DD4 File Offset: 0x00084FD4
		public MethodReturnValue MethodReturnValue { get; private set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x00086DDD File Offset: 0x00084FDD
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x00086DE5 File Offset: 0x00084FE5
		public bool UseExtendedResult { get; private set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x00086DEE File Offset: 0x00084FEE
		// (set) Token: 0x06002603 RID: 9731 RVA: 0x00086DF6 File Offset: 0x00084FF6
		public Type InnerResultType { get; private set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x00086DFF File Offset: 0x00084FFF
		// (set) Token: 0x06002605 RID: 9733 RVA: 0x00086E07 File Offset: 0x00085007
		public string InnerResultPropertyName { get; private set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x00086E10 File Offset: 0x00085010
		// (set) Token: 0x06002607 RID: 9735 RVA: 0x00086E18 File Offset: 0x00085018
		public Dictionary<string, string> PropertyToHeaderDictionary { get; private set; }

		// Token: 0x06002608 RID: 9736 RVA: 0x00086E21 File Offset: 0x00085021
		private bool IsOperationContractMethod()
		{
			return ExtendedReflection.IsCustomAttributePresent(this.Method, typeof(OperationContractAttribute).FullName);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x00086E3D File Offset: 0x0008503D
		private bool IsOperationContractAsyncMethod()
		{
			return ContractMethod.IsOperationContractAsyncMethod(this.Method);
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x00086E4A File Offset: 0x0008504A
		private bool IsOperationContractTaskAsyncMethod()
		{
			return ContractMethod.IsOperationContractTaskAsyncMethod(this.Method);
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x00086E58 File Offset: 0x00085058
		private static bool IsOperationContractAsyncMethod(MethodInfo method)
		{
			object parameterValueFromCustomAttribute = ExtendedReflection.GetParameterValueFromCustomAttribute(method, typeof(OperationContractAttribute).FullName, "AsyncPattern");
			return parameterValueFromCustomAttribute is bool && (bool)parameterValueFromCustomAttribute;
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x00086E90 File Offset: 0x00085090
		private static bool IsOperationContractTaskAsyncMethod(MethodInfo method)
		{
			return method.ReturnType != null && (method.ReturnType.Equals(typeof(Task)) || method.ReturnType.IsSubclassOf(typeof(Task)));
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x00086ED0 File Offset: 0x000850D0
		private void ParseMethod()
		{
			this.GetMethodReturnValue();
			if (!this.IsOperationContractMethod())
			{
				this.MethodType = ContractMethodType.NotOperationContract;
				if (this.Method.Name.StartsWith("End", StringComparison.Ordinal))
				{
					string text = this.Method.Name.Substring("End".Length, this.Method.Name.Length - "End".Length);
					foreach (MethodInfo methodInfo in this.Com.GetContractMethods())
					{
						if (ContractMethod.IsOperationContractAsyncMethod(methodInfo) && methodInfo.Name.Length >= "Begin".Length + 1)
						{
							string text2 = methodInfo.Name.Substring("Begin".Length, methodInfo.Name.Length - "Begin".Length);
							if (text.Equals(text2, StringComparison.Ordinal))
							{
								this.MethodType = ContractMethodType.OperationContractEnd;
								break;
							}
						}
					}
				}
				return;
			}
			foreach (ParameterInfo parameterInfo in this.Method.GetParameters())
			{
				if (ExtendedReflection.GetBaseCustomAttribute(CustomAttributeData.GetCustomAttributes(parameterInfo), typeof(ECFRemovableParamAttribute).FullName) != null)
				{
					this.RemovableParameters.Add(new ECFParameter(parameterInfo, true));
				}
				CustomAttributeData customAttribute = ExtendedReflection.GetCustomAttribute(CustomAttributeData.GetCustomAttributes(parameterInfo), typeof(ECFKeyAttribute).FullName);
				CustomAttributeData customAttribute2 = ExtendedReflection.GetCustomAttribute(CustomAttributeData.GetCustomAttributes(parameterInfo), typeof(ECFRemovableKeyAttribute).FullName);
				CustomAttributeData customAttribute3 = ExtendedReflection.GetCustomAttribute(CustomAttributeData.GetCustomAttributes(parameterInfo), typeof(ECFHttpHeaderAttribute).FullName);
				CustomAttributeData customAttribute4 = ExtendedReflection.GetCustomAttribute(CustomAttributeData.GetCustomAttributes(parameterInfo), typeof(ECFSoapHeaderAttribute).FullName);
				if (customAttribute != null || customAttribute2 != null)
				{
					if (customAttribute != null && customAttribute2 != null)
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "On Contract '{0}': Parameter '{1}' in method '{2}' cannot be decorated with both 'ECFKey' and 'ECFRemovableKey' attributes", new object[]
						{
							this.Com.Contract.Name,
							parameterInfo.Name,
							this.Method.Name
						}));
					}
					this.BalancingKeys.Add(new ECFParameter(parameterInfo, customAttribute2 != null));
				}
				else if (customAttribute3 != null)
				{
					this.HeaderHttpParameters.Add(new ECFParameter(parameterInfo, false));
				}
				else if (customAttribute4 != null)
				{
					this.HeaderSoapParameters.Add(new ECFParameter(parameterInfo, false));
				}
			}
			if (this.IsOperationContractOneWayMethod())
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The operation '{0}' in contract '{1}' must not be marked as 'IsOneWay=true' in the 'OperationContract' attribute", new object[]
				{
					this.Method.Name,
					this.Com.Contract.Name
				}));
			}
			if (this.IsOperationContractAsyncMethod())
			{
				this.MethodType = ContractMethodType.OperationContractBegin;
				return;
			}
			if (this.IsOperationContractTaskAsyncMethod())
			{
				this.MethodType = ContractMethodType.OperationContractTaskAsync;
				return;
			}
			this.MethodType = ContractMethodType.OperationContractSync;
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000871B8 File Offset: 0x000853B8
		private bool IsOperationContractOneWayMethod()
		{
			object parameterValueFromCustomAttribute = ExtendedReflection.GetParameterValueFromCustomAttribute(ExtendedReflection.GetCustomAttribute(CustomAttributeData.GetCustomAttributes(this.Method), typeof(OperationContractAttribute).FullName), "IsOneWay");
			return parameterValueFromCustomAttribute is bool && (bool)parameterValueFromCustomAttribute;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000871FF File Offset: 0x000853FF
		private void GetMethodReturnValue()
		{
			this.MethodReturnValue = (this.Method.ReturnType.Name.Equals("Void", StringComparison.Ordinal) ? MethodReturnValue.Void : MethodReturnValue.NonVoid);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x00087228 File Offset: 0x00085428
		private void ExtendedResultAnalysis()
		{
			if (!this.Method.ReturnType.GetCustomAttributes(typeof(ECFExtendedResultAttribute), true).Any<object>())
			{
				this.UseExtendedResult = false;
				this.InnerResultType = this.Method.ReturnType;
				return;
			}
			this.UseExtendedResult = true;
			IEnumerable<PropertyInfo> enumerable = from p in this.Method.ReturnType.GetProperties()
				where p.GetCustomAttributes(typeof(ECFResultAttribute), true).Any<object>()
				select p;
			if (enumerable.Count<PropertyInfo>() > 1)
			{
				throw new CommunicationFrameworkAmbigiousResultAttributeException(this.Method.ReturnType.Name);
			}
			if (enumerable.Any<PropertyInfo>())
			{
				this.InnerResultType = enumerable.First<PropertyInfo>().PropertyType;
				this.InnerResultPropertyName = enumerable.First<PropertyInfo>().Name;
			}
			if (this.Method.ReturnType.GetProperties().Any((PropertyInfo p) => p.GetCustomAttributes(typeof(ECFResponseHeaderAttribute), true).Any<object>() && !(p.PropertyType == typeof(string))))
			{
				throw new CommunicationFrameworkNonStringHeadersException();
			}
			this.PropertyToHeaderDictionary = new Dictionary<string, string>();
			foreach (PropertyInfo propertyInfo in this.Method.ReturnType.GetProperties())
			{
				object obj = propertyInfo.GetCustomAttributes(typeof(ECFResponseHeaderAttribute), true).FirstOrDefault<object>();
				if (obj != null)
				{
					this.PropertyToHeaderDictionary.Add(propertyInfo.Name, ((ECFResponseHeaderAttribute)obj).HeaderName);
				}
			}
		}

		// Token: 0x04000D77 RID: 3447
		private const string EndString = "End";

		// Token: 0x04000D78 RID: 3448
		private const string BeginString = "Begin";
	}
}
