const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:19679';

const PROXY_CONFIG = [
  {
    context: [
     "/**",
   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    },
   

    bypass: function (req, res, proxyOptions) {
      if (!req.originalUrl.includes('api')) {
        console.log('Skipping proxy for browser request.');
       return '/index.html';
      }
      req.headers['X-Custom-Header'] = 'yes';
    }
  }
]

module.exports = PROXY_CONFIG;
