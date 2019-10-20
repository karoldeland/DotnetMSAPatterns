
# Port foward grafana
$grafanaPod = kubectl -n istio-system get pod -l app=grafana -o jsonpath='{.items[0].metadata.name}'
kubectl -n istio-system port-forward $grafanaPod 3000:3000

