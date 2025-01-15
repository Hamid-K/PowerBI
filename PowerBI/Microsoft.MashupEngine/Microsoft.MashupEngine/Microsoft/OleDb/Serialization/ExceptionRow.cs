using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FB5 RID: 8117
	public class ExceptionRow : IExceptionRow
	{
		// Token: 0x0600C60C RID: 50700 RVA: 0x00277719 File Offset: 0x00275919
		public ExceptionRow(IDictionary<int, ISerializedException> exceptions)
		{
			this.exceptions = exceptions;
		}

		// Token: 0x17003013 RID: 12307
		// (get) Token: 0x0600C60D RID: 50701 RVA: 0x00277728 File Offset: 0x00275928
		public IDictionary<int, ISerializedException> Exceptions
		{
			get
			{
				return this.exceptions;
			}
		}

		// Token: 0x0400652E RID: 25902
		private IDictionary<int, ISerializedException> exceptions;
	}
}
