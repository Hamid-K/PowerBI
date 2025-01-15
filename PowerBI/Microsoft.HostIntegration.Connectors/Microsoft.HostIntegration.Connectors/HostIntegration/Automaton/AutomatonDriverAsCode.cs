using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.HostIntegration.StrictResources.AutomatonDriver;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.Automaton
{
	// Token: 0x020004C5 RID: 1221
	public abstract class AutomatonDriverAsCode : AsynchronousConnectionClient, IDynamicDataBufferOwner
	{
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x0007D306 File Offset: 0x0007B506
		public override string Name
		{
			get
			{
				return this.automaton.Name;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600299F RID: 10655 RVA: 0x0007D313 File Offset: 0x0007B513
		// (set) Token: 0x060029A0 RID: 10656 RVA: 0x0007D31B File Offset: 0x0007B51B
		public AutomatonDefinition Automaton
		{
			get
			{
				return this.automaton;
			}
			protected set
			{
				this.automaton = value;
			}
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x0007D324 File Offset: 0x0007B524
		public static void RegisterAutomaton(int numberOfAutomatons, int automatonType, int numberOfConnections)
		{
			if (AutomatonDriverAsCode.automatonTypeToNumberOfLocations == null)
			{
				object syncStartup = AutomatonDriverAsCode.SyncStartup;
				lock (syncStartup)
				{
					if (AutomatonDriverAsCode.automatonTypeToNumberOfLocations == null)
					{
						int[] array = new int[numberOfAutomatons];
						for (int i = 0; i < numberOfAutomatons; i++)
						{
							array[i] = -1;
						}
						AutomatonDriverAsCode.blockingCollections = new BlockingCollection<ConnectionLocation>[numberOfAutomatons][];
						AutomatonDriverAsCode.automatonTypeToNumberOfLocations = array;
					}
				}
			}
			AutomatonDriverAsCode.automatonTypeToNumberOfLocations[automatonType] = numberOfConnections;
			AutomatonDriverAsCode.blockingCollections[automatonType] = new BlockingCollection<ConnectionLocation>[numberOfConnections];
			for (int j = 0; j < numberOfConnections; j++)
			{
				AutomatonDriverAsCode.blockingCollections[automatonType][j] = new BlockingCollection<ConnectionLocation>();
				new Thread(new ParameterizedThreadStart(AutomatonDriverAsCode.ReceiveMessagesThreadProc))
				{
					IsBackground = true
				}.Start(AutomatonDriverAsCode.blockingCollections[automatonType][j]);
			}
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x0007D3F0 File Offset: 0x0007B5F0
		protected AutomatonDriverAsCode(int automatonType)
			: this(automatonType, 0)
		{
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x0007D3FC File Offset: 0x0007B5FC
		protected AutomatonDriverAsCode(int automatonType, int numberOfSubAutomatons)
		{
			this.buffers = new List<DynamicDataBuffer>();
			base..ctor();
			int num = AutomatonDriverAsCode.automatonTypeToNumberOfLocations[automatonType];
			this.connectionLocations = new ConnectionLocation[num];
			this.subAutomatons = new AutomatonDriverAsCode[numberOfSubAutomatons + 1];
			this.subAutomatons[0] = this;
			this.automatonType = automatonType;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x0007D44C File Offset: 0x0007B64C
		protected AutomatonDriverAsCode(AutomatonDriverAsCode parent)
		{
			this.buffers = new List<DynamicDataBuffer>();
			base..ctor();
			this.parentAutomaton = parent;
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x0007D466 File Offset: 0x0007B666
		protected void AddSubAutomaton(AutomatonDriverAsCode subAutomaton, int subAutomatonIndex)
		{
			this.subAutomatons[subAutomatonIndex] = subAutomaton;
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x0007D471 File Offset: 0x0007B671
		protected void AddConnection(int connectionEnumeration, int expectedOtherEnd, ProcessAsynchronousMessage messageDelegate)
		{
			this.AddConnections(connectionEnumeration, expectedOtherEnd, messageDelegate, null, ConnectionType.OneToOne, 1);
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x0007D47F File Offset: 0x0007B67F
		protected void AddConnections(int connectionEnumeration, int expectedOtherEnd, ProcessAsynchronousMessage messageDelegate, int maximumConnections)
		{
			if (maximumConnections == 1)
			{
				this.AddConnections(connectionEnumeration, expectedOtherEnd, messageDelegate, null, ConnectionType.One, 1);
				return;
			}
			this.AddConnections(connectionEnumeration, expectedOtherEnd, messageDelegate, null, ConnectionType.Many, maximumConnections);
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x0007D4A0 File Offset: 0x0007B6A0
		protected void AddConnections(int connectionEnumeration, int expectedOtherEnd, ProcessAsynchronousMessage messageDelegate, ProcessDisconnect disconnectDelegate, int maximumConnections)
		{
			this.AddConnections(connectionEnumeration, expectedOtherEnd, messageDelegate, disconnectDelegate, ConnectionType.Many, maximumConnections);
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x0007D4B0 File Offset: 0x0007B6B0
		private void AddConnections(int connectionEnumeration, int expectedOtherEnd, ProcessAsynchronousMessage messageDelegate, ProcessDisconnect disconnectDelegate, ConnectionType connectionType, int maximumConnections)
		{
			ConnectionLocation connectionLocation = new ConnectionLocation(this, messageDelegate, disconnectDelegate, connectionEnumeration, expectedOtherEnd, connectionType, maximumConnections);
			ConnectionLocation[] array = this.connectionLocations;
			lock (array)
			{
				this.connectionLocations[connectionEnumeration] = connectionLocation;
			}
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x0007D504 File Offset: 0x0007B704
		protected void ProcessEvent(int eventToProcess)
		{
			this.ProcessEvent(0, eventToProcess);
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x0007D510 File Offset: 0x0007B710
		protected void ProcessEvent(int subAutomatonIndex, int eventToProcess)
		{
			AutomatonDefinition automatonDefinition = this.Automaton;
			FlagBasedTracePoint automatonTracePoint = automatonDefinition.Context.AutomatonTracePoint;
			AutomatonDefinition automatonDefinition2 = automatonDefinition;
			lock (automatonDefinition2)
			{
				try
				{
					bool flag2 = true;
					while (flag2)
					{
						int num = automatonDefinition.Context.CurrentState[0].Process(ref eventToProcess);
						StateAsCodeDriver stateAsCodeDriver = automatonDefinition.StatesAsCode[num];
						if (eventToProcess < 0)
						{
							flag2 = false;
						}
						automatonDefinition.Context.CurrentState[0] = stateAsCodeDriver;
					}
				}
				catch (Exception ex)
				{
					if (automatonTracePoint.IsEnabled(TraceFlags.Error))
					{
						automatonTracePoint.Trace(TraceFlags.Error, ex);
					}
					automatonDefinition.Context.AutomatonEventLog.WriteEvent(SR.AutomatonProcessingFailed(this.Name, ex.Message), EventLogEntryType.Error);
					throw new InvalidProgramException("Automaton: " + this.Name + " - Processing Failed", ex);
				}
			}
			if (automatonTracePoint.IsEnabled(TraceFlags.Debug))
			{
				automatonTracePoint.Trace(TraceFlags.Debug, string.Format("Finished processing Event", Array.Empty<object>()));
			}
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x0007D620 File Offset: 0x0007B820
		protected void ConnectTo(int connectionEnumeration, AutomatonDriverAsCode otherEnd)
		{
			ConnectionLocation connectionLocation = null;
			ConnectionLocation[] array = this.connectionLocations;
			lock (array)
			{
				connectionLocation = this.connectionLocations[connectionEnumeration];
			}
			connectionLocation.ConnectTo(otherEnd, connectionEnumeration);
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x0007D670 File Offset: 0x0007B870
		protected void ConnectTo(int connectionEnumeration, AutomatonDriverAsCode otherEnd, int determinant)
		{
			this.connectionLocations[connectionEnumeration].ConnectTo(connectionEnumeration, otherEnd, determinant);
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x0007D684 File Offset: 0x0007B884
		protected void Send(int connectionEnumeration, AsynchronousConnectionMessage message)
		{
			ConnectionLocation connectionLocation = null;
			ConnectionLocation[] array = this.connectionLocations;
			lock (array)
			{
				connectionLocation = this.connectionLocations[connectionEnumeration];
			}
			connectionLocation.Send(message);
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x0007D6D0 File Offset: 0x0007B8D0
		protected bool Send(int connectionEnumeration, AsynchronousConnectionMessage message, int determinant)
		{
			ConnectionLocation connectionLocation = null;
			ConnectionLocation[] array = this.connectionLocations;
			lock (array)
			{
				connectionLocation = this.connectionLocations[connectionEnumeration];
			}
			return connectionLocation.Send(message, determinant);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x0007D720 File Offset: 0x0007B920
		protected void DisconnectFrom(int connectionEnumeration)
		{
			ConnectionLocation connectionLocation = null;
			ConnectionLocation[] array = this.connectionLocations;
			lock (array)
			{
				connectionLocation = this.connectionLocations[connectionEnumeration];
			}
			connectionLocation.DisconnectFrom(connectionEnumeration);
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x0007D76C File Offset: 0x0007B96C
		protected void DisconnectFrom(int connectionEnumeration, int determinant, bool needToInformOtherEnd)
		{
			ConnectionLocation connectionLocation = null;
			ConnectionLocation[] array = this.connectionLocations;
			lock (array)
			{
				connectionLocation = this.connectionLocations[connectionEnumeration];
			}
			connectionLocation.DisconnectFrom(connectionEnumeration, determinant, needToInformOtherEnd);
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x0007D7BC File Offset: 0x0007B9BC
		internal ConnectionLocation GetConnectionLocation(int enumeration)
		{
			ConnectionLocation[] array = this.connectionLocations;
			ConnectionLocation connectionLocation;
			lock (array)
			{
				connectionLocation = this.connectionLocations[enumeration];
			}
			return connectionLocation;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x0007D800 File Offset: 0x0007BA00
		public DynamicDataBuffer GetBuffer()
		{
			return this.GetBuffer(0);
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x0007D80C File Offset: 0x0007BA0C
		public DynamicDataBuffer GetBuffer(int capacity)
		{
			List<DynamicDataBuffer> list = this.buffers;
			DynamicDataBuffer dynamicDataBuffer;
			lock (list)
			{
				if (this.buffers.Count == 0)
				{
					dynamicDataBuffer = new DynamicDataBuffer(this, capacity);
				}
				else
				{
					int num = 0;
					int num2 = capacity;
					for (int i = 0; i < this.buffers.Count; i++)
					{
						if (this.buffers[i].Data.Length >= capacity)
						{
							DynamicDataBuffer dynamicDataBuffer2 = this.buffers[i];
							this.buffers.RemoveAt(i);
							return dynamicDataBuffer2;
						}
						if (this.buffers[i].Data.Length < num2)
						{
							num2 = this.buffers[i].Data.Length;
							num = i;
						}
					}
					if (this.buffers.Count > 10)
					{
						DynamicDataBuffer dynamicDataBuffer3 = this.buffers[num];
						this.buffers.RemoveAt(num);
						dynamicDataBuffer3.Resize(capacity);
						dynamicDataBuffer = dynamicDataBuffer3;
					}
					else
					{
						dynamicDataBuffer = new DynamicDataBuffer(this, capacity);
					}
				}
			}
			return dynamicDataBuffer;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x0007D924 File Offset: 0x0007BB24
		public void ReturnDataBuffer(DynamicDataBuffer buffer)
		{
			List<DynamicDataBuffer> list = this.buffers;
			lock (list)
			{
				this.buffers.Add(buffer);
			}
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x0007D96C File Offset: 0x0007BB6C
		public void ClearBuffers()
		{
			List<DynamicDataBuffer> list = this.buffers;
			lock (list)
			{
				foreach (DynamicDataBuffer dynamicDataBuffer in this.buffers)
				{
					dynamicDataBuffer.ClearOwner();
				}
				this.buffers.Clear();
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x0007D9F0 File Offset: 0x0007BBF0
		public override void MessageReceived(ConnectionLocation connectionLocation)
		{
			BlockingCollection<ConnectionLocation> blockingCollection = AutomatonDriverAsCode.blockingCollections[this.automatonType][connectionLocation.ConnectionEnumeration];
			try
			{
				blockingCollection.Add(connectionLocation);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x0007DA30 File Offset: 0x0007BC30
		private static void ReceiveMessagesThreadProc(object context)
		{
			BlockingCollection<ConnectionLocation> blockingCollection = (BlockingCollection<ConnectionLocation>)context;
			bool flag = true;
			while (flag)
			{
				try
				{
					blockingCollection.Take().ProcessAnyReceivedMessages();
				}
				catch (Exception)
				{
					flag = false;
				}
			}
		}

		// Token: 0x04001883 RID: 6275
		private AutomatonDefinition automaton;

		// Token: 0x04001884 RID: 6276
		private ConnectionLocation[] connectionLocations;

		// Token: 0x04001885 RID: 6277
		private AutomatonDriverAsCode parentAutomaton;

		// Token: 0x04001886 RID: 6278
		private AutomatonDriverAsCode[] subAutomatons;

		// Token: 0x04001887 RID: 6279
		private List<DynamicDataBuffer> buffers;

		// Token: 0x04001888 RID: 6280
		private static object SyncStartup = new object();

		// Token: 0x04001889 RID: 6281
		private int automatonType;

		// Token: 0x0400188A RID: 6282
		private static int[] automatonTypeToNumberOfLocations;

		// Token: 0x0400188B RID: 6283
		private static BlockingCollection<ConnectionLocation>[][] blockingCollections;
	}
}
