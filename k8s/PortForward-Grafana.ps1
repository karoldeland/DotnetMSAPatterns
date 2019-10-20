
# Port foward grafana
$grafanaPod = kubectl -n istio-system get pod -l app=grafana -o jsonpath='{.items[0].metadata.name}'
kubectl -n istio-system port-forward $grafanaPod 3000:3000

# Port forward prometheus
#kubectl -n istio-system port-forward $(kubectl -n istio-system get pod -l app=prometheus -o jsonpath='{.items[0].metadata.name}') 9090:9090

# Port forward jaeger
#kubectl port-forward -n istio-system $(kubectl get pod -n istio-system -l app=jaeger -o jsonpath='{.items[0].metadata.name}') 16686:16686

# Port forward kiali
#kubectl port-forward -n istio-system $(kubectl get pod -n istio-system -l app=kiali -o jsonpath='{.items[0].metadata.name}') 20001:20001