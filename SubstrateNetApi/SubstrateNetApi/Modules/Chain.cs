﻿/// <file> SubstrateNetApi\Modules\Chain.cs </file>
/// <copyright file="Chain.cs" company="mogwaicoin.org">
/// Copyright (c) 2020 mogwaicoin.org. All rights reserved.
/// </copyright>
/// <summary> Implements the chain class. </summary>
using SubstrateNetApi.MetaDataModel.Values;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SubstrateNetApi.Modules
{
    /// <summary> A chain. </summary>
    /// <remarks> 19.09.2020. </remarks>
    public class Chain
    {
        /// <summary> The client. </summary>
        private readonly SubstrateClient _client;

        /// <summary> Constructor. </summary>
        /// <remarks> 19.09.2020. </remarks>
        /// <param name="client"> The client. </param>
        internal Chain(SubstrateClient client)
        {
            _client = client;
        }

        /// <summary> Gets block asynchronous. </summary>
        /// <remarks> 19.09.2020. </remarks>
        /// <returns> The block. </returns>
        public async Task<BlockDataStr> GetBlockAsync()
        {
            return await GetBlockAsync(null, CancellationToken.None);
        }

        /// <summary> Gets block asynchronous. </summary>
        /// <remarks> 19.09.2020. </remarks>
        /// <param name="token"> A token that allows processing to be cancelled. </param>
        /// <returns> The block. </returns>
        public async Task<BlockDataStr> GetBlockAsync(CancellationToken token)
        {
            return await GetBlockAsync(null, token);
        }

        /// <summary> Gets block asynchronous. </summary>
        /// <remarks> 19.09.2020. </remarks>
        /// <param name="hash"> The hash. </param>
        /// <returns> The block. </returns>
        public async Task<BlockDataStr> GetBlockAsync(byte[] hash)
        {
            return await GetBlockAsync(hash, CancellationToken.None);
        }

        /// <summary> Gets block asynchronous. </summary>
        /// <remarks> 19.09.2020. </remarks>
        /// <param name="hash">  The hash. </param>
        /// <param name="token"> A token that allows processing to be cancelled. </param>
        /// <returns> The block. </returns>
        public async Task<BlockDataStr> GetBlockAsync(byte[] hash, CancellationToken token)
        {

            var parameter = hash != null ? Utils.Bytes2HexString(hash) : null;

            return await _client.InvokeAsync<BlockDataStr>("chain_getBlock", new object[] { parameter }, token);
        }

        /// <summary> Gets block hash asynchronous. </summary>
        /// <remarks> 19.09.2020. </remarks>
        /// <returns> The block hash. </returns>
        public async Task<string> GetBlockHashAsync()
        {
            return await GetBlockHashAsync(null, CancellationToken.None);
        }

        /// <summary> Gets block hash asynchronous. </summary>
        /// <remarks> 19.09.2020. </remarks>
        /// <param name="BlockNumber"> The block number. </param>
        /// <returns> The block hash. </returns>
        public async Task<string> GetBlockHashAsync(uint BlockNumber)
        {
            return await GetBlockHashAsync(BlockNumber, CancellationToken.None);
        }

        /// <summary> Gets block hash asynchronous. </summary>
        /// <remarks> 19.09.2020. </remarks>
        /// <param name="token"> A token that allows processing to be cancelled. </param>
        /// <returns> The block hash. </returns>
        public async Task<string> GetBlockHashAsync(CancellationToken token)
        {
            return await GetBlockHashAsync(null, token);
        }

        /// <summary> Gets block hash asynchronous. </summary>
        /// <remarks> 19.09.2020. </remarks>
        /// <param name="blockNumber"> The block number. </param>
        /// <param name="token">       A token that allows processing to be cancelled. </param>
        /// <returns> The block hash. </returns>
        public async Task<string> GetBlockHashAsync(uint? blockNumber, CancellationToken token)
        {

            var parameter = blockNumber.HasValue ? Utils.Bytes2HexString(BitConverter.GetBytes(blockNumber.Value)) : null;
          
            return await _client.InvokeAsync<string>("chain_getBlockHash", new object[] { parameter }, token);
        }
    }
}
