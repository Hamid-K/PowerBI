using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Hosting;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000A0 RID: 160
	[Guid("FBB03F30-DECD-4a40-9E8F-06ACB99A0A97")]
	[Designer("Microsoft.DataWarehouse.Design.ModelComponentDesigner, Microsoft.DataWarehouse.AS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", typeof(IDesigner))]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	public abstract class ModelComponent : Component, IModelComponent, IComponent, IDisposable, IHostableComponent
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00025DB0 File Offset: 0x00023FB0
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00025DB8 File Offset: 0x00023FB8
		IModelComponentCollection IModelComponent.OwningCollection
		{
			get
			{
				return this.OwningCollection;
			}
			set
			{
				this.owningCollection = (ModelComponentCollection)value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00025DC6 File Offset: 0x00023FC6
		string IModelComponent.FriendlyPath
		{
			get
			{
				return this.FriendlyPath;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600079D RID: 1949
		protected internal abstract string KeyForCollection { get; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600079E RID: 1950
		protected internal abstract string FriendlyName { get; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00025DCE File Offset: 0x00023FCE
		internal string FriendlyPath
		{
			get
			{
				return base.GetType().Name + " " + this.GetObjectPath();
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00025DEC File Offset: 0x00023FEC
		internal virtual ModelType GetModelType()
		{
			ModelComponent modelComponent = this.Parent as ModelComponent;
			if (modelComponent != null)
			{
				return modelComponent.GetModelType();
			}
			return ModelType.Default;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00025E10 File Offset: 0x00024010
		internal virtual int GetCompatibilityLevel()
		{
			ModelComponent modelComponent = this.Parent as ModelComponent;
			if (modelComponent != null)
			{
				return modelComponent.GetCompatibilityLevel();
			}
			return 0;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00025E34 File Offset: 0x00024034
		internal void ValidateCompatibility(ModelType modelType, int compatibilityLevel)
		{
			string text;
			if (!this.IsCompatible(modelType, compatibilityLevel, out text))
			{
				throw new InvalidOperationException(text);
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00025E54 File Offset: 0x00024054
		internal virtual bool IsCompatible(ModelType modelType, int compatibilityLevel, out string error)
		{
			error = null;
			throw new NotImplementedException();
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00025E60 File Offset: 0x00024060
		internal string GetObjectPath()
		{
			StringCollection stringCollection = new StringCollection();
			stringCollection.Add(this.FriendlyName);
			ModelComponent modelComponent = this.Parent as ModelComponent;
			while (modelComponent != null && !(modelComponent is Database) && !(modelComponent is Server))
			{
				stringCollection.Add(modelComponent.FriendlyName);
				modelComponent = modelComponent.Parent as ModelComponent;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = stringCollection.Count - 1; i >= 0; i--)
			{
				stringBuilder.Append('[');
				stringBuilder.Append(stringCollection[i]);
				stringBuilder.Append(']');
				if (i > 0)
				{
					stringBuilder.Append('.');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00025F06 File Offset: 0x00024106
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00025F0E File Offset: 0x0002410E
		[XmlIgnore]
		[Browsable(false)]
		public ModelComponentCollection OwningCollection
		{
			get
			{
				return this.owningCollection;
			}
			set
			{
				this.owningCollection = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00025F17 File Offset: 0x00024117
		[XmlIgnore]
		[Browsable(false)]
		public IModelComponent Parent
		{
			get
			{
				if (this.OwningCollection == null)
				{
					return null;
				}
				return this.OwningCollection.Parent;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00025F2E File Offset: 0x0002412E
		[Browsable(false)]
		[XmlArray(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
		public virtual AnnotationCollection Annotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00025F36 File Offset: 0x00024136
		public virtual void Submit(bool submitPermanently)
		{
			IHostMaterializationService hostMaterializationService = (IHostMaterializationService)this.GetHostService(typeof(IHostMaterializationService));
			if (hostMaterializationService == null)
			{
				throw new InvalidOperationException(SR.Collections_NoServiceForAddNew(base.GetType()));
			}
			hostMaterializationService.UpdateMaterialization(this, submitPermanently);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00025F68 File Offset: 0x00024168
		public virtual void Submit()
		{
			this.Submit(true);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00025F71 File Offset: 0x00024171
		protected void CopyTo(ModelComponent obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (obj == this)
			{
				throw new InvalidOperationException(SR.Copy_DestinationIsSelf);
			}
			this.Annotations.CopyTo(obj.Annotations);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00025FA1 File Offset: 0x000241A1
		protected void Reset()
		{
			this.annotations.Clear();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00025FAE File Offset: 0x000241AE
		protected internal virtual void AfterInsert(int index)
		{
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00025FB0 File Offset: 0x000241B0
		protected internal virtual void AfterMove(int fromIndex, int toIndex)
		{
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00025FB2 File Offset: 0x000241B2
		protected internal virtual void BeforeRemove(bool cleanUp)
		{
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00025FB4 File Offset: 0x000241B4
		protected internal virtual void AfterRemove(ModelComponentCollection collection)
		{
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00025FB8 File Offset: 0x000241B8
		protected internal virtual void AddToContainer(IContainer container)
		{
			if (this.IsContainerVolatile(container))
			{
				return;
			}
			if (base.Container == null)
			{
				container.Add(this);
			}
			if (this is IMajorObject && !((IMajorObject)this).IsLoaded)
			{
				return;
			}
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties())
			{
				if (propertyInfo.GetCustomAttributes(typeof(XmlIgnoreAttribute), false).Length == 0)
				{
					Type propertyType = propertyInfo.PropertyType;
					if (null != propertyType.GetInterface(typeof(IComponent).Name) && propertyInfo.CanWrite)
					{
						this.AddToContainer(propertyInfo.GetValue(this, null), container);
					}
					else if (null != propertyType.GetInterface(typeof(ICollection).Name))
					{
						object value = propertyInfo.GetValue(this, null);
						IEnumerable enumerable = (IEnumerable)value;
						if (enumerable != null)
						{
							IOnDemandLoadableCollection onDemandLoadableCollection = value as IOnDemandLoadableCollection;
							try
							{
								if (onDemandLoadableCollection != null)
								{
									onDemandLoadableCollection.BlockOnDemandLoad(true);
								}
								foreach (object obj in enumerable)
								{
									this.AddToContainer(obj, container);
								}
							}
							finally
							{
								if (onDemandLoadableCollection != null)
								{
									onDemandLoadableCollection.BlockOnDemandLoad(false);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00026120 File Offset: 0x00024320
		private void AddToContainer(object obj, IContainer container)
		{
			if (obj == null)
			{
				return;
			}
			if (obj is ModelComponent)
			{
				((ModelComponent)obj).AddToContainer(container);
			}
			else
			{
				if (obj is IComponent && ((IComponent)obj).Site == null)
				{
					container.Add((IComponent)obj);
				}
				if (obj is IDataItem)
				{
					IBinding source = ((IDataItem)obj).Source;
					if (source != null && source.Site == null)
					{
						container.Add(source);
					}
				}
			}
			if (obj is IEnumerable)
			{
				foreach (object obj2 in ((IEnumerable)obj))
				{
					this.AddToContainer(obj2, container);
				}
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000261E0 File Offset: 0x000243E0
		protected internal virtual void RemoveFromContainer(IContainer container)
		{
			container.Remove(this);
			if (this is IMajorObject && !((IMajorObject)this).IsLoaded)
			{
				return;
			}
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties())
			{
				if (propertyInfo.GetCustomAttributes(typeof(XmlIgnoreAttribute), false).Length == 0)
				{
					Type propertyType = propertyInfo.PropertyType;
					if (null != propertyType.GetInterface(typeof(IComponent).Name) && propertyInfo.CanWrite)
					{
						this.RemoveFromContainer(propertyInfo.GetValue(this, null), container);
					}
					else if (null != propertyType.GetInterface(typeof(ICollection).Name))
					{
						IEnumerable enumerable = (IEnumerable)propertyInfo.GetValue(this, null);
						if (enumerable != null)
						{
							foreach (object obj in enumerable)
							{
								this.RemoveFromContainer(obj, container);
							}
						}
					}
				}
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00026300 File Offset: 0x00024500
		private void RemoveFromContainer(object obj, IContainer container)
		{
			if (obj == null)
			{
				return;
			}
			if (obj is ModelComponent)
			{
				((ModelComponent)obj).RemoveFromContainer(container);
			}
			else if (obj is IDataItem)
			{
				container.Remove((IComponent)obj);
				IBinding source = ((IDataItem)obj).Source;
				if (source != null)
				{
					container.Remove(source);
				}
			}
			else if (obj is IComponent)
			{
				container.Remove((IComponent)obj);
			}
			if (obj is IEnumerable)
			{
				foreach (object obj2 in ((IEnumerable)obj))
				{
					this.RemoveFromContainer(obj2, container);
				}
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000263B8 File Offset: 0x000245B8
		public bool Validate(ValidationErrorCollection errors)
		{
			return this.Validate(errors, true);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000263C4 File Offset: 0x000245C4
		public bool Validate(ValidationErrorCollection errors, bool includeDetailedErrors)
		{
			Server server = null;
			for (IModelComponent modelComponent = this; modelComponent != null; modelComponent = modelComponent.Parent)
			{
				server = modelComponent as Server;
				if (server != null)
				{
					break;
				}
			}
			if (server == null)
			{
				throw new AmoException(ValidationSR.ParentServerIsMissing);
			}
			return this.Validate(errors, includeDetailedErrors, server.Edition);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00026407 File Offset: 0x00024607
		public virtual bool Validate(ValidationErrorCollection errors, bool includeDetailedErrors, ServerEdition serverEdition)
		{
			if (errors == null)
			{
				throw new ArgumentNullException("errors");
			}
			if (this is Database || this is Server)
			{
				return true;
			}
			if (this.Parent == null)
			{
				errors.Add(this, ValidationSR.ParentIsMissing);
				return false;
			}
			return true;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00026444 File Offset: 0x00024644
		private bool IsContainerVolatile(IContainer container)
		{
			if (container == null)
			{
				return true;
			}
			IDesignerHost designerHost = container as IDesignerHost;
			return designerHost != null && designerHost.Loading;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00026468 File Offset: 0x00024668
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x00026470 File Offset: 0x00024670
		IServiceProvider IHostableComponent.Host
		{
			get
			{
				return this.host;
			}
			set
			{
				this.host = value;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0002647C File Offset: 0x0002467C
		internal object GetHostService(Type serviceType)
		{
			object obj = null;
			for (IModelComponent modelComponent = this; modelComponent != null; modelComponent = modelComponent.Parent)
			{
				IHostableComponent hostableComponent = modelComponent as IHostableComponent;
				if (modelComponent != null)
				{
					IServiceProvider serviceProvider = hostableComponent.Host;
					if (serviceProvider != null)
					{
						obj = serviceProvider.GetService(serviceType);
						if (obj != null)
						{
							break;
						}
					}
				}
			}
			if (obj == null)
			{
				return null;
			}
			return obj;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000264BE File Offset: 0x000246BE
		public override string ToString()
		{
			return this.FriendlyName;
		}

		// Token: 0x04000496 RID: 1174
		private ModelComponentCollection owningCollection;

		// Token: 0x04000497 RID: 1175
		private AnnotationCollection annotations = new AnnotationCollection();

		// Token: 0x04000498 RID: 1176
		private IServiceProvider host;
	}
}
