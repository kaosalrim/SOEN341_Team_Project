# service/scripts/ci.sh

#!/usr/bin/env bash

set -e

npm ci
npm run test