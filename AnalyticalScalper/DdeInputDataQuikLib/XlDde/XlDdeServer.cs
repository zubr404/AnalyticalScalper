// ==========================================================================
//    XlDdeServer.cs (c) 2011 Nikolay Moroshkin, http://www.moroshkin.com/
// ==========================================================================

using System;
using System.Collections.Generic;

using NDde.Server;

namespace XlDde
{
  // ************************************************************************
  // *                          XlDdeChannel class                          *
  // ************************************************************************

  abstract class XlDdeChannel
  {
    public virtual bool IsConnected { get; set; }
    public DateTime DataReceived { get; protected set; }

    public bool IsError { get; protected set; }
    public void ResetError() { IsError = false; }

    public void PutDdeData(byte[] data)
    {
      DataReceived = DateTime.UtcNow;

      using(XlTable xt = new XlTable(data))
        ProcessTable(xt);
    }

    protected abstract void ProcessTable(XlTable xt);
  }

  // ************************************************************************
  // *                          XlDdeServer class                           *
  // ************************************************************************

  sealed class XlDdeServer : DdeServer
  {
    Dictionary<string, XlDdeChannel> channels;

    // --------------------------------------------------------------

    public XlDdeServer(string service)
      : base(service)
    {
      channels = new Dictionary<string, XlDdeChannel>();
    }

    // --------------------------------------------------------------

    public void AddChannel(string topic, XlDdeChannel channel)
    {
      channels.Add(topic, channel);
    }

    // --------------------------------------------------------------

    protected override bool OnBeforeConnect(string topic)
    {
      return channels.ContainsKey(topic);
    }

    // --------------------------------------------------------------

    protected override void OnAfterConnect(DdeConversation c)
    {
      XlDdeChannel channel = channels[c.Topic];
      c.Tag = channel;
      channel.IsConnected = true;
    }

    // --------------------------------------------------------------

    protected override void OnDisconnect(DdeConversation c)
    {
      ((XlDdeChannel)c.Tag).IsConnected = false;
    }

    // --------------------------------------------------------------

    protected override PokeResult OnPoke(DdeConversation c, string item, byte[] data, int format)
    {
      //if(format != xlTableFormat)
      //  return PokeResult.NotProcessed;

      ((XlDdeChannel)c.Tag).PutDdeData(data);
      return PokeResult.Processed;
    }

    // --------------------------------------------------------------
  }

  // ************************************************************************
}
