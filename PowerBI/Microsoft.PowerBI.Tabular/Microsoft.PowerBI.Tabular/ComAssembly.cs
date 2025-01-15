using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000D2 RID: 210
	[Guid("EB79D51C-7F27-46f6-A089-43DD46C6D6F3")]
	public sealed class ComAssembly : Assembly, IMajorObject
	{
		// Token: 0x06000D41 RID: 3393 RVA: 0x0006CABE File Offset: 0x0006ACBE
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0006CAC6 File Offset: 0x0006ACC6
		private protected override MajorObject.MajorObjectBody CreateBodyImpl()
		{
			return new ComAssembly.ComAssemblyBody(this);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0006CAD0 File Offset: 0x0006ACD0
		protected internal override MajorObject Clone(bool forceBodyLoading)
		{
			MajorObject majorObject = new ComAssembly();
			this.CopyTo(majorObject, forceBodyLoading);
			return majorObject;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0006CAEC File Offset: 0x0006ACEC
		protected internal override void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			ComAssembly comAssembly = destination as ComAssembly;
			if (comAssembly == null)
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			base.CopyTo(destination, forceBodyLoading);
			if (!base.IsLoaded && !forceBodyLoading)
			{
				return;
			}
			comAssembly.Source = this.Source;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0006CB41 File Offset: 0x0006AD41
		public ComAssembly()
		{
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0006CB49 File Offset: 0x0006AD49
		public ComAssembly(string name)
			: base(name)
		{
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0006CB52 File Offset: 0x0006AD52
		public ComAssembly(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0006CB5C File Offset: 0x0006AD5C
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x0006CB69 File Offset: 0x0006AD69
		[XmlElement(IsNullable = false)]
		[LocalizedDescription("PropertyDescription_ComAssembly_Source")]
		public string Source
		{
			get
			{
				return base.GetBody<ComAssembly.ComAssemblyBody>().Source;
			}
			set
			{
				base.GetBody<ComAssembly.ComAssemblyBody>().Source = value;
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0006CB77 File Offset: 0x0006AD77
		public ComAssembly CopyTo(ComAssembly obj)
		{
			this.CopyTo(obj, true);
			return obj;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0006CB82 File Offset: 0x0006AD82
		public override Assembly Clone()
		{
			return this.CopyTo(new ComAssembly());
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0006CB8F File Offset: 0x0006AD8F
		internal override bool IsSyntacticallyValidID(string newValue, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidID(newValue, type, out error);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0006CB99 File Offset: 0x0006AD99
		internal override bool IsValidName(string newValue, Type type, ModelType modelType, int compatibilityLevel, NamedComponentCollection namedComponentCollection, out string error)
		{
			return Utils.IsValidName(newValue, type, modelType, compatibilityLevel, namedComponentCollection, out error);
		}

		// Token: 0x020002E9 RID: 745
		private sealed class ComAssemblyBody : Assembly.AssemblyBody
		{
			// Token: 0x060023B0 RID: 9136 RVA: 0x000E293F File Offset: 0x000E0B3F
			public ComAssemblyBody(ComAssembly owner)
				: base(owner)
			{
				this.Source = null;
			}

			// Token: 0x04000AB9 RID: 2745
			public string Source;
		}
	}
}
