﻿using Microsoft.Extensions.Logging;
using SA.Irrigation.Common.Helpers;
using SA.Irrigation.Common.Models.QueueItems;
using SA.Irrigation.Common.Services;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Services.Implementation
{
    public class LoraQueueProcessor : IQueueProcessor
    {
        private readonly ILogger<LoraQueueProcessor> _logger;
        private readonly ITransportLayer _transportLayer;

        private Queue<SendItem> _sendQueue = new Queue<SendItem>();

        public LoraQueueProcessor(ILogger<LoraQueueProcessor> logger, ITransportLayer transportLayer)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(transportLayer, nameof(transportLayer));
            _logger = logger;
            _transportLayer = transportLayer;
        }

        public void Enqueue(SendItem item)
        {
            _sendQueue.Enqueue(item);
        }

        public void ProcessQueue()
        {
            ProcessSendQueue();
        }

        private void ProcessSendQueue()
        {
            if (_sendQueue.Count > 0)
            {
                while (_sendQueue.Count > 0) 
                {
                    var item = _sendQueue.Dequeue();
                    _transportLayer.SendData(BuildDataToSend(item, (_transportLayer as LoraTransportLayer).Channel));
                    Thread.Sleep(200);
                }
                (_transportLayer as LoraTransportLayer).Close();
            }
        }

        private byte[] BuildDataToSend(SendItem item, int channel)
        {
            var str = $"{((byte)(item.Address >> 8)).ToString("X2")} {((byte)(item.Address & 0xff)).ToString("X2")} {channel.ToString("X2")} {item.Message}";
            return HexStrToBytes.ToBytes(str);
        }
    }
}
