dotnet publish ProductCategories/ProductCategories.csproj -c Debug -o publish

docker build --tag productcategories:latest --no-cache -f Dockerfile .

docker tag productcategories:latest buketbm/productcategories

docker push buketbm/productcategories