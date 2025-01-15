using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.Mashup.ProviderCommon;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000034 RID: 52
	public sealed class MashupParameterCollection : DbParameterCollection
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000AFB5 File Offset: 0x000091B5
		public MashupParameterCollection()
		{
			this.parameters = new List<MashupParameter>();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000AFC8 File Offset: 0x000091C8
		public void Add(MashupParameter parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException("parameter");
			}
			this.parameters.Add(parameter);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public override int Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is MashupParameter))
			{
				throw new ArgumentException("parameter must be a MashupParameter", "value");
			}
			int count = this.parameters.Count;
			this.Add((MashupParameter)value);
			return count;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B024 File Offset: 0x00009224
		public override void AddRange(Array values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			foreach (object obj in values)
			{
				this.Add(obj);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B084 File Offset: 0x00009284
		public override void Clear()
		{
			this.parameters.Clear();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B091 File Offset: 0x00009291
		public override bool Contains(string value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000B098 File Offset: 0x00009298
		public override bool Contains(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000B09F File Offset: 0x0000929F
		public override void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000B0A6 File Offset: 0x000092A6
		public override int Count
		{
			get
			{
				return this.parameters.Count;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000B0B3 File Offset: 0x000092B3
		public override IEnumerator GetEnumerator()
		{
			return this.parameters.GetEnumerator();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B0C5 File Offset: 0x000092C5
		protected override DbParameter GetParameter(string parameterName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B0CC File Offset: 0x000092CC
		protected override DbParameter GetParameter(int index)
		{
			return this.parameters[index];
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B0DA File Offset: 0x000092DA
		public override int IndexOf(string parameterName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B0E1 File Offset: 0x000092E1
		public override int IndexOf(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B0E8 File Offset: 0x000092E8
		public override void Insert(int index, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B0EF File Offset: 0x000092EF
		public override bool IsFixedSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B0F6 File Offset: 0x000092F6
		public override bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000B0FD File Offset: 0x000092FD
		public override bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B104 File Offset: 0x00009304
		public override void Remove(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B10B File Offset: 0x0000930B
		public override void RemoveAt(string parameterName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B112 File Offset: 0x00009312
		public override void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B119 File Offset: 0x00009319
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B120 File Offset: 0x00009320
		protected override void SetParameter(int index, DbParameter value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000B127 File Offset: 0x00009327
		public override object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000B12E File Offset: 0x0000932E
		internal IList<IParameter> ToIParameterList()
		{
			return this.parameters.Select((MashupParameter parameter) => parameter.ToIParameter()).ToList<IParameter>();
		}

		// Token: 0x04000165 RID: 357
		private readonly List<MashupParameter> parameters;
	}
}
