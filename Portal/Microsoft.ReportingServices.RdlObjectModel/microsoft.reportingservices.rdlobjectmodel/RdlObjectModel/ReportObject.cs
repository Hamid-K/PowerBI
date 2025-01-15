using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000C7 RID: 199
	public abstract class ReportObject : ReportObjectBase
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0001CB4C File Offset: 0x0001AD4C
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x0001CB54 File Offset: 0x0001AD54
		[XmlElement(typeof(ComponentMetadata), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/componentdefinition")]
		[DefaultValue(null)]
		public ComponentMetadata ComponentMetadata
		{
			get
			{
				return this.m_componentMetadata;
			}
			set
			{
				if (this.m_componentMetadata != value)
				{
					this.m_componentMetadata = value;
					if (this.m_componentMetadata != null)
					{
						this.m_componentMetadata.Parent = this;
					}
				}
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001CB7A File Offset: 0x0001AD7A
		protected ReportObject()
		{
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001CB82 File Offset: 0x0001AD82
		internal ReportObject(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001CB8C File Offset: 0x0001AD8C
		public virtual object DeepClone()
		{
			Type type = base.GetType();
			PropertyStore propertyStore = new PropertyStore();
			ReportObject reportObject = (ReportObject)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { propertyStore }, null);
			propertyStore.SetOwner(reportObject);
			this.CopyTo(reportObject, null);
			return reportObject;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001CBD0 File Offset: 0x0001ADD0
		private void CopyTo(ReportObject clone, ICollection<string> membersToExclude)
		{
			foreach (MemberMapping memberMapping in ((StructMapping)TypeMapper.GetTypeMapping(base.GetType())).Members)
			{
				if (memberMapping.HasValue(this) && (membersToExclude == null || !membersToExclude.Contains(memberMapping.Name)))
				{
					object value = memberMapping.GetValue(this);
					memberMapping.SetValue(clone, ReportObject.CloneObject(value));
				}
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001CC5C File Offset: 0x0001AE5C
		protected static object CloneObject(object obj)
		{
			if (obj is ReportObject)
			{
				obj = ((ReportObject)obj).DeepClone();
			}
			else if (obj is IList)
			{
				obj = ReportObject.CloneList((IList)obj);
			}
			return obj;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001CC8C File Offset: 0x0001AE8C
		private static object CloneList(IList obj)
		{
			IList list = (IList)Activator.CreateInstance(obj.GetType());
			foreach (object obj2 in obj)
			{
				list.Add(ReportObject.CloneObject(obj2));
			}
			return list;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001CCF4 File Offset: 0x0001AEF4
		internal virtual void OnSetObject(int propertyIndex)
		{
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
		internal T GetAncestor<T>() where T : class
		{
			for (IContainedObject containedObject = base.Parent; containedObject != null; containedObject = containedObject.Parent)
			{
				if (containedObject is T)
				{
					return (T)((object)containedObject);
				}
			}
			return default(T);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001CD30 File Offset: 0x0001AF30
		internal virtual void UpdateNamedReferences(NameChanges nameChanges)
		{
			if (nameChanges.Count > 0)
			{
				Dictionary<int, IExpression> updatedExpressions = new Dictionary<int, IExpression>();
				base.PropertyStore.IterateObjectEntries(delegate(int propertyIndex, object value)
				{
					if (value is ReportExpression)
					{
						ReportExpression reportExpression = ((ReportExpression)value).UpdateNamedReferences(nameChanges);
						if (!string.Equals(reportExpression.Value, ((ReportExpression)value).Value, StringComparison.Ordinal))
						{
							updatedExpressions.Add(propertyIndex, reportExpression);
							return;
						}
					}
					else if (value is ReportExpression<bool>)
					{
						ReportExpression<bool> reportExpression2 = ((ReportExpression<bool>)value).UpdateNamedReferences(nameChanges);
						if (!string.Equals(reportExpression2.Expression, ((ReportExpression<bool>)value).Expression, StringComparison.Ordinal))
						{
							updatedExpressions.Add(propertyIndex, reportExpression2);
							return;
						}
					}
					else if (value is ReportExpression<long>)
					{
						ReportExpression<long> reportExpression3 = ((ReportExpression<long>)value).UpdateNamedReferences(nameChanges);
						if (!string.Equals(reportExpression3.Expression, ((ReportExpression<long>)value).Expression, StringComparison.Ordinal))
						{
							updatedExpressions.Add(propertyIndex, reportExpression3);
							return;
						}
					}
					else if (value is ReportExpression<double>)
					{
						ReportExpression<double> reportExpression4 = ((ReportExpression<double>)value).UpdateNamedReferences(nameChanges);
						if (!string.Equals(reportExpression4.Expression, ((ReportExpression<double>)value).Expression, StringComparison.Ordinal))
						{
							updatedExpressions.Add(propertyIndex, reportExpression4);
							return;
						}
					}
					else
					{
						if (value is ReportObject)
						{
							((ReportObject)value).UpdateNamedReferences(nameChanges);
							return;
						}
						if (value is IList)
						{
							foreach (object obj in ((IList)value))
							{
								if (obj is ReportObject)
								{
									((ReportObject)obj).UpdateNamedReferences(nameChanges);
								}
							}
						}
					}
				});
				foreach (int num in updatedExpressions.Keys)
				{
					base.PropertyStore.SetObject(num, updatedExpressions[num]);
				}
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001CDD8 File Offset: 0x0001AFD8
		internal IList<ReportObject> GetDependencies()
		{
			IList<ReportObject> list = new List<ReportObject>();
			this.GetDependenciesCore(list);
			return list;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001CDF4 File Offset: 0x0001AFF4
		protected virtual void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.PropertyStore.IterateObjectEntries(delegate(int propertyIndex, object value)
			{
				if (value is IExpression)
				{
					((IExpression)value).GetDependencies(dependencies, this);
					return;
				}
				if (value is ReportObject)
				{
					((ReportObject)value).GetDependenciesCore(dependencies);
					return;
				}
				if (value is IList)
				{
					foreach (object obj in ((IList)value))
					{
						if (obj is ReportObject)
						{
							((ReportObject)obj).GetDependenciesCore(dependencies);
						}
						else if (obj is ReportExpression)
						{
							((ReportExpression)obj).GetDependencies(dependencies, this);
						}
					}
				}
			});
		}

		// Token: 0x0400017B RID: 379
		private ComponentMetadata m_componentMetadata;
	}
}
